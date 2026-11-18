using B2BWholesale.Core.Data;
using B2BWholesale.Core.Models;
using B2BWholesale.Core.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace B2BWholesale.Tests
{
    public class BusinessLogicTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly B2BContext _db;
        private readonly OrderService _orderService;
        private readonly AuthService _authService;

        public BusinessLogicTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<B2BContext>()
                .UseSqlite(_connection)
                .Options;

            _db = new B2BContext(options);
            _db.Database.EnsureCreated();

            // Seed dữ liệu sạch độc lập cho mỗi test case
            _db.Agents.Add(new Agent
            {
                AgentID = 1,
                AgentName = "Đại lý Kiểm thử Minh Phát",
                Address = "Quận 7, TP.HCM",
                Phone = "0901234501"
            });

            _db.Items.AddRange(
                new Item { ItemID = 1, ItemName = "Lavie 500ml", Size = "Thùng 24 chai", Price = 85000m, StockQuantity = 50 },
                new Item { ItemID = 2, ItemName = "Coca-Cola 320ml", Size = "Thùng 24 lon", Price = 195000m, StockQuantity = 10 }
            );

            _db.Users.AddRange(
                new AppUser { UserID = 1, UserName = "admin", email = "admin@test.vn", password = "123", IsLocked = false, Role = "Admin" },
                new AppUser { UserID = 2, UserName = "locked_agent", email = "locked@test.vn", password = "123", IsLocked = true, Role = "Agent", AgentID = 1 },
                new AppUser { UserID = 3, UserName = "agent1", email = "agent1@test.vn", password = "123", IsLocked = false, Role = "Agent", AgentID = 1 }
            );

            _db.SaveChanges();

            _orderService = new OrderService(_db);
            _authService = new AuthService(_db);
        }

        [Fact]
        public async Task Test1_PlaceOrder_EnoughStock_CommitsAndDeductsStock()
        {
            var lines = new List<OrderLineInput> { new(1, 15), new(2, 4) };
            var result = await _orderService.PlaceOrderWithTransactionAsync(1, lines);

            Assert.True(result.Success);
            Assert.NotNull(result.OrderId);
            Assert.Equal(35, (await _db.Items.FindAsync(1))!.StockQuantity);
            Assert.Equal(6, (await _db.Items.FindAsync(2))!.StockQuantity);
            Assert.Equal(1, await _db.Orders.CountAsync());
            Assert.Equal(2, await _db.OrderDetails.CountAsync());
        }

        [Fact]
        public async Task Test2_PlaceOrder_ExactStock_SucceedsAndStockBecomesZero()
        {
            var lines = new List<OrderLineInput> { new(2, 10) };
            var result = await _orderService.PlaceOrderWithTransactionAsync(1, lines);

            Assert.True(result.Success);
            Assert.Equal(0, (await _db.Items.FindAsync(2))!.StockQuantity);
        }

        [Fact]
        public async Task Test3_PlaceOrder_InsufficientStockOnSecondLine_RollsBackEntireTransaction()
        {
            // Dòng 1 hợp lệ (10 <= 50), nhưng dòng 2 vượt tồn kho (25 > 10) -> Phải rollback toàn bộ
            var lines = new List<OrderLineInput> { new(1, 10), new(2, 25) };
            var result = await _orderService.PlaceOrderWithTransactionAsync(1, lines);

            Assert.False(result.Success);
            Assert.Null(result.OrderId);
            Assert.Equal(50, (await _db.Items.FindAsync(1))!.StockQuantity); // Không bị trừ nhầm
            Assert.Equal(10, (await _db.Items.FindAsync(2))!.StockQuantity);
            Assert.Equal(0, await _db.Orders.CountAsync());
            Assert.Equal(0, await _db.OrderDetails.CountAsync());
        }

        [Fact]
        public async Task Test4_PlaceOrder_DuplicateItemExceedingCombinedStock_BlocksOrder()
        {
            // Item 2 có tồn kho = 10; đặt 2 dòng trùng nhau 6 + 6 = 12 (> 10) -> Phải chặn
            var lines = new List<OrderLineInput> { new(2, 6), new(2, 6) };
            var result = await _orderService.PlaceOrderWithTransactionAsync(1, lines);

            Assert.False(result.Success);
            Assert.Equal(10, (await _db.Items.FindAsync(2))!.StockQuantity);
            Assert.Equal(0, await _db.Orders.CountAsync());
        }

        [Fact]
        public async Task Test5_PlaceOrder_ZeroOrNegativeQuantity_ReturnsValidationError()
        {
            var lines = new List<OrderLineInput> { new(1, -5) };
            var result = await _orderService.PlaceOrderWithTransactionAsync(1, lines);

            Assert.False(result.Success);
            Assert.Equal(50, (await _db.Items.FindAsync(1))!.StockQuantity);
        }

        [Fact]
        public void Test6_CalculateOrderTotal_MultipleDetails_ReturnsExactTotalAmount()
        {
            var details = new List<OrderDetail>
            {
                new() { ItemID = 1, Quantity = 10, UnitAmount = 85000m },
                new() { ItemID = 2, Quantity = 4, UnitAmount = 195000m }
            };

            decimal total = _orderService.CalculateOrderTotal(details);
            Assert.Equal(1630000m, total);
        }

        [Fact]
        public async Task Test7_Authenticate_LockedAccount_BlocksLogin()
        {
            var result = await _authService.AuthenticateAsync("locked_agent", "123", "Agent");

            Assert.False(result.Success);
            Assert.Null(result.User);
        }

        [Fact]
        public async Task Test8_Authenticate_RoleMismatch_BlocksAgentFromAdminAppAndAdminFromWeb()
        {
            var agentOnWinForms = await _authService.AuthenticateAsync("agent1", "123", "Admin");
            var adminOnWeb = await _authService.AuthenticateAsync("admin", "123", "Agent");

            Assert.False(agentOnWinForms.Success);
            Assert.False(adminOnWeb.Success);
        }

        public void Dispose()
        {
            _db.Dispose();
            _connection.Dispose();
        }
    }
}