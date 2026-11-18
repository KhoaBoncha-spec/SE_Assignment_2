using B2BWholesale.Core.Data;
using B2BWholesale.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B2BWholesale.Web.Controllers
{
    [Authorize(Roles = "Agent")]
    public class OrdersController : Controller
    {
        private readonly B2BContext _db;
        private readonly OrderService _orderService;

        public OrdersController(B2BContext db, OrderService orderService)
        {
            _db = db;
            _orderService = orderService;
        }

        private int GetCurrentAgentId() =>
            int.Parse(User.FindFirst("AgentID")?.Value ?? "0");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(List<int> itemIds, List<int> quantities)
        {
            var lines = new List<OrderLineInput>();
            if (itemIds != null && quantities != null)
            {
                int count = Math.Min(itemIds.Count, quantities.Count);
                for (int i = 0; i < count; i++)
                {
                    lines.Add(new OrderLineInput(itemIds[i], quantities[i]));
                }
            }

            var result = await _orderService.PlaceOrderWithTransactionAsync(GetCurrentAgentId(), lines);
            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index", "Catalog");
            }

            TempData["Success"] = $"Đặt đơn hàng #{result.OrderId} thành công! Tồn kho đã được tự động khấu trừ.";
            return RedirectToAction(nameof(MyOrders));
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            int agentId = GetCurrentAgentId();
            var orders = await _db.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Item)
                .Where(o => o.AgentID == agentId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Invoice(int id)
        {
            int agentId = GetCurrentAgentId();
            var order = await _db.Orders
                .Include(o => o.Agent)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Item)
                .FirstOrDefaultAsync(o => o.OrderID == id && o.AgentID == agentId);

            if (order == null)
                return Forbid();

            return View(order);
        }
    }
}