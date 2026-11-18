using System.Diagnostics;
using B2BWholesale.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace B2BWholesale.WinForms
{
    public partial class MainAdminForm : Form
    {
        private int _selectedItemId = 0;
        private int _selectedAgentId = 0;

        public MainAdminForm()
        {
            InitializeComponent();
            this.Load += MainAdminForm_Load;
        }

        private async void MainAdminForm_Load(object? sender, EventArgs e)
        {
            await LoadItemsAsync();
            await LoadAgentsAsync();
            await LoadOrdersAsync();
            await LoadDashboardAsync();
        }

        // =========================================================
        // TAB 1: QUẢN LÝ HÀNG HÓA (ITEM CRUD - TASK 1.2)
        // =========================================================
        private async Task LoadItemsAsync()
        {
            using var db = DbFactory.CreateContext();
            dgvItems.DataSource = await db.Items.OrderBy(i => i.ItemID).ToListAsync();
        }

        // Gán sự kiện SelectionChanged hoặc CellClick của dgvItems vào hàm này
        private void dgvItems_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (dgvItems.CurrentRow == null) return;
            _selectedItemId = Convert.ToInt32(dgvItems.CurrentRow.Cells["ItemID"].Value ?? 0);
            txtItemName.Text = dgvItems.CurrentRow.Cells["ItemName"].Value?.ToString() ?? "";
            txtItemSize.Text = dgvItems.CurrentRow.Cells["Size"].Value?.ToString() ?? "";
            numItemPrice.Value = Convert.ToDecimal(dgvItems.CurrentRow.Cells["Price"].Value ?? 0);
            numItemStock.Value = Convert.ToInt32(dgvItems.CurrentRow.Cells["StockQuantity"].Value ?? 0);
        }

        private async void btnAddItem_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text) || numItemPrice.Value <= 0)
            {
                MessageBox.Show("Tên hàng không được trống và giá phải lớn hơn 0.");
                return;
            }

            using var db = DbFactory.CreateContext();
            db.Items.Add(new Item
            {
                ItemName = txtItemName.Text.Trim(),
                Size = txtItemSize.Text.Trim(),
                Price = numItemPrice.Value,
                StockQuantity = (int)numItemStock.Value
            });
            await db.SaveChangesAsync();
            MessageBox.Show("Thêm mặt hàng thành công!");
            await LoadItemsAsync();
            await LoadDashboardAsync();
        }

        private async void btnUpdateItem_Click(object? sender, EventArgs e)
        {
            if (_selectedItemId == 0) { MessageBox.Show("Hãy chọn mặt hàng cần cập nhật."); return; }

            using var db = DbFactory.CreateContext();
            var item = await db.Items.FindAsync(_selectedItemId);
            if (item == null) return;

            item.ItemName = txtItemName.Text.Trim();
            item.Size = txtItemSize.Text.Trim();
            item.Price = numItemPrice.Value;
            item.StockQuantity = (int)numItemStock.Value;

            await db.SaveChangesAsync();
            MessageBox.Show("Cập nhật mặt hàng & tồn kho thành công!");
            await LoadItemsAsync();
            await LoadDashboardAsync();
        }

        private async void btnDeleteItem_Click(object? sender, EventArgs e)
        {
            if (_selectedItemId == 0) return;
            try
            {
                using var db = DbFactory.CreateContext();
                var item = await db.Items.FindAsync(_selectedItemId);
                if (item == null) return;
                db.Items.Remove(item);
                await db.SaveChangesAsync();
                _selectedItemId = 0;
                await LoadItemsAsync();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Không thể xóa mặt hàng này vì đã phát sinh trong đơn hàng (ràng buộc khóa ngoại).",
                    "Ràng buộc toàn vẹn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =========================================================
        // TAB 2: QUẢN LÝ ĐẠI LÝ (AGENT CRUD - TASK 1.2)
        // =========================================================
        private async Task LoadAgentsAsync()
        {
            using var db = DbFactory.CreateContext();
            var agents = await db.Agents.OrderBy(a => a.AgentID).ToListAsync();
            dgvAgents.DataSource = agents;

            // Đổ danh sách đại lý vào ComboBox lọc của Tab Dashboard
            var filterList = new List<Agent> { new Agent { AgentID = 0, AgentName = "-- Tất cả đại lý --" } };
            filterList.AddRange(agents);
            cboFilterAgent.DataSource = filterList;
            cboFilterAgent.DisplayMember = "AgentName";
            cboFilterAgent.ValueMember = "AgentID";
        }

        private void dgvAgents_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (dgvAgents.CurrentRow == null) return;
            _selectedAgentId = Convert.ToInt32(dgvAgents.CurrentRow.Cells["AgentID"].Value ?? 0);
            txtAgentName.Text = dgvAgents.CurrentRow.Cells["AgentName"].Value?.ToString() ?? "";
            txtAgentAddress.Text = dgvAgents.CurrentRow.Cells["Address"].Value?.ToString() ?? "";
            txtAgentPhone.Text = dgvAgents.CurrentRow.Cells["Phone"].Value?.ToString() ?? "";
        }

        private async void btnAddAgent_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAgentName.Text)) return;
            using var db = DbFactory.CreateContext();
            db.Agents.Add(new Agent
            {
                AgentName = txtAgentName.Text.Trim(),
                Address = txtAgentAddress.Text.Trim(),
                Phone = txtAgentPhone.Text.Trim()
            });
            await db.SaveChangesAsync();
            MessageBox.Show("Thêm đại lý thành công!");
            await LoadAgentsAsync();
        }

        private async void btnUpdateAgent_Click(object? sender, EventArgs e)
        {
            if (_selectedAgentId == 0) return;
            using var db = DbFactory.CreateContext();
            var agent = await db.Agents.FindAsync(_selectedAgentId);
            if (agent == null) return;

            agent.AgentName = txtAgentName.Text.Trim();
            agent.Address = txtAgentAddress.Text.Trim();
            agent.Phone = txtAgentPhone.Text.Trim();
            await db.SaveChangesAsync();
            MessageBox.Show("Cập nhật thông tin đại lý thành công!");
            await LoadAgentsAsync();
        }

        private async void btnDeleteAgent_Click(object? sender, EventArgs e)
        {
            if (_selectedAgentId == 0) return;
            try
            {
                using var db = DbFactory.CreateContext();
                var agent = await db.Agents.FindAsync(_selectedAgentId);
                if (agent == null) return;
                db.Agents.Remove(agent);
                await db.SaveChangesAsync();
                _selectedAgentId = 0;
                await LoadAgentsAsync();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Không thể xóa đại lý đã có lịch sử đơn hàng (ON DELETE NO ACTION).",
                    "Ràng buộc toàn vẹn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =========================================================
        // TAB 3: QUẢN LÝ ĐƠN HÀNG & SHIPPING MANIFEST (TASK 1.2 & 1.3)
        // =========================================================
        private async Task LoadOrdersAsync()
        {
            using var db = DbFactory.CreateContext();
            var orders = await db.Orders
                .Include(o => o.Agent)
                .Include(o => o.OrderDetails)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            dgvOrders.DataSource = orders.Select(o => new
            {
                o.OrderID,
                NgayDat = o.OrderDate.ToString("dd/MM/yyyy HH:mm"),
                DaiLy = o.Agent?.AgentName ?? "N/A",
                TrangThai = o.OrderStatus,
                TongTien = o.OrderDetails.Sum(d => d.Quantity * d.UnitAmount).ToString("N0") + " đ"
            }).ToList();
        }

        private async void btnUpdateStatus_Click(object? sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow == null) return;
            string? newStatus = cboOrderStatus.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(newStatus))
            {
                MessageBox.Show("Vui lòng chọn trạng thái mới.");
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.CurrentRow.Cells["OrderID"].Value ?? 0);
            using var db = DbFactory.CreateContext();
            var order = await db.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.OrderStatus = newStatus;
                await db.SaveChangesAsync();
                MessageBox.Show($"Đã cập nhật đơn #{orderId} sang trạng thái {newStatus}!");
                await LoadOrdersAsync();
                await LoadDashboardAsync();
            }
        }

        private async void btnPrintManifest_Click(object? sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để in.");
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.CurrentRow.Cells["OrderID"].Value ?? 0);
            using var db = DbFactory.CreateContext();
            var order = await db.Orders
                .Include(o => o.Agent)
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Item)
                .FirstOrDefaultAsync(o => o.OrderID == orderId);

            if (order != null)
            {
                decimal totalAmount = order.OrderDetails.Sum(d => d.Quantity * d.UnitAmount);
                GenerateShippingManifestHtml(order, totalAmount);
            }
        }

        private void GenerateShippingManifestHtml(Order order, decimal totalAmount)
        {
            var rows = string.Join("", order.OrderDetails.Select((d, idx) =>
                $"<tr><td>{idx + 1}</td><td>{d.Item?.ItemName}</td><td>{d.Item?.Size}</td>" +
                $"<td>{d.Quantity}</td><td>{d.UnitAmount:N0} đ</td><td>{(d.Quantity * d.UnitAmount):N0} đ</td></tr>"));

            string html = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'/>
    <title>Shipping Manifest - Order #{order.OrderID}</title>
    <style>
        body {{ font-family: Arial, sans-serif; margin: 40px; }}
        .header {{ display: flex; justify-content: space-between; border-bottom: 2px solid #333; padding-bottom: 10px; }}
        table {{ width: 100%; border-collapse: collapse; margin-top: 20px; }}
        th, td {{ border: 1px solid #ccc; padding: 10px; text-align: left; }}
        th {{ background: #f4f4f4; }}
        .total {{ text-align: right; font-size: 1.2em; font-weight: bold; margin-top: 15px; }}
        @media print {{ .no-print {{ display: none; }} }}
    </style>
</head>
<body>
    <button class='no-print' onclick='window.print()' style='padding:8px 16px;margin-bottom:20px;'>In / Lưu PDF</button>
    <div class='header'>
        <div>
            <h2>PHIẾU XUẤT KHO (SHIPPING MANIFEST)</h2>
            <p><strong>Mã đơn hàng:</strong> #{order.OrderID} | <strong>Ngày đặt:</strong> {order.OrderDate:dd/MM/yyyy HH:mm}</p>
            <p><strong>Trạng thái:</strong> {order.OrderStatus}</p>
        </div>
        <div>
            <p><strong>Đại lý nhận hàng:</strong> {order.Agent?.AgentName}</p>
            <p><strong>Địa chỉ giao:</strong> {order.Agent?.Address}</p>
            <p><strong>Điện thoại:</strong> {order.Agent?.Phone}</p>
        </div>
    </div>
    <table>
        <thead>
            <tr><th>#</th><th>Tên mặt hàng</th><th>Quy cách</th><th>Số lượng</th><th>Đơn giá</th><th>Thành tiền</th></tr>
        </thead>
        <tbody>{rows}</tbody>
    </table>
    <div class='total'>Tổng giá trị lô hàng: {totalAmount:N0} VNĐ</div>
</body>
</html>";

            string filePath = Path.Combine(Path.GetTempPath(), $"ShippingManifest_Order_{order.OrderID}.html");
            File.WriteAllText(filePath, html);
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }

        // =========================================================
        // TAB 4: DASHBOARD & DATA FILTER (TASK 1.4)
        // =========================================================
        private async void btnRefreshDashboard_Click(object? sender, EventArgs e)
        {
            await LoadDashboardAsync();
        }

        private async Task LoadDashboardAsync()
        {
            using var db = DbFactory.CreateContext();
            int threshold = (int)numStockThreshold.Value;
            int selectedAgentId = cboFilterAgent.SelectedValue is int id ? id : 0;

            // 1. Best-selling items
            dgvBestSelling.DataSource = await db.OrderDetails
                .Where(od => od.Order != null && od.Order.OrderStatus != "Cancelled")
                .GroupBy(od => new { od.ItemID, ItemName = od.Item != null ? od.Item.ItemName : "" })
                .Select(g => new
                {
                    g.Key.ItemID,
                    g.Key.ItemName,
                    TotalSold = g.Sum(x => x.Quantity),
                    TotalRevenue = g.Sum(x => x.Quantity * x.UnitAmount)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(10)
                .ToListAsync();

            // 2. Low-stock items warning
            dgvLowStock.DataSource = await db.Items
                .Where(i => i.StockQuantity <= threshold)
                .OrderBy(i => i.StockQuantity)
                .Select(i => new { i.ItemID, i.ItemName, i.Size, i.StockQuantity })
                .ToListAsync();

            // 3. Total revenue by specific agents
            dgvRevenueByAgent.DataSource = await db.Orders
                .Where(o => o.OrderStatus != "Cancelled" && (selectedAgentId == 0 || o.AgentID == selectedAgentId))
                .GroupBy(o => new { o.AgentID, AgentName = o.Agent != null ? o.Agent.AgentName : "" })
                .Select(g => new
                {
                    g.Key.AgentID,
                    g.Key.AgentName,
                    OrderCount = g.Count(),
                    TotalRevenue = g.SelectMany(o => o.OrderDetails).Sum(od => od.Quantity * od.UnitAmount)
                })
                .OrderByDescending(x => x.TotalRevenue)
                .ToListAsync();
        }
    }
}