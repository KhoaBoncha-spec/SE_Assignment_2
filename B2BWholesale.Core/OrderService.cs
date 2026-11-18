using B2BWholesale.Core.Data;
using B2BWholesale.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace B2BWholesale.Core.Services
{
    public class OrderService
    {
        private readonly B2BContext _db;
        public OrderService(B2BContext db) => _db = db;

        public decimal CalculateOrderTotal(IEnumerable<OrderDetail> details)
        {
            if (details == null) return 0m;
            return details.Sum(d => d.Quantity * d.UnitAmount);
        }

        /// <summary>
        /// Task 3.2: Tạo Order, thêm OrderDetails và trừ tồn kho trong CÙNG MỘT Database Transaction.
        /// Đáp ứng toàn bộ 8 kịch bản kiểm thử trong sheet 'Transaction Tests' của Team Workbook.
        /// </summary>
        public async Task<(bool Success, string Message, int? OrderId)> PlaceOrderWithTransactionAsync(
            int agentId, List<OrderLineInput> lines)
        {
            if (lines == null || lines.Count == 0)
                return (false, "Đơn hàng phải có ít nhất 1 mặt hàng.", null);

            if (lines.Any(l => l.Quantity <= 0))
                return (false, "Số lượng đặt hàng phải lớn hơn 0.", null);

            // Gộp các dòng trùng ItemID để kiểm tra tổng số lượng tồn kho chính xác (Scenario 6)
            var groupedLines = lines
                .GroupBy(l => l.ItemID)
                .Select(g => new OrderLineInput(g.Key, g.Sum(x => x.Quantity)))
                .ToList();

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var order = new Order
                {
                    AgentID = agentId,
                    OrderDate = DateTime.Now,
                    OrderStatus = "Pending"
                };

                _db.Orders.Add(order);
                await _db.SaveChangesAsync();

                foreach (var line in groupedLines)
                {
                    var item = await _db.Items.FirstOrDefaultAsync(i => i.ItemID == line.ItemID);
                    if (item == null)
                    {
                        await transaction.RollbackAsync();
                        _db.ChangeTracker.Clear();
                        return (false, $"Không tìm thấy mặt hàng mã #{line.ItemID}.", null);
                    }

                    if (item.StockQuantity < line.Quantity)
                    {
                        await transaction.RollbackAsync();
                        _db.ChangeTracker.Clear();
                        return (false, $"Mặt hàng '{item.ItemName}' không đủ tồn kho (Tồn: {item.StockQuantity}, Đặt: {line.Quantity}).", null);
                    }

                    // Trừ tồn kho
                    item.StockQuantity -= line.Quantity;

                    // Thêm chi tiết đơn hàng
                    _db.OrderDetails.Add(new OrderDetail
                    {
                        OrderID = order.OrderID,
                        ItemID = item.ItemID,
                        Quantity = line.Quantity,
                        UnitAmount = item.Price
                    });
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                return (true, "Đặt hàng thành công!", order.OrderID);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _db.ChangeTracker.Clear();
                return (false, $"Lỗi giao dịch, đã hoàn tác (Rollback): {ex.Message}", null);
            }
        }
    }
}