#nullable disable
using System.Drawing;
using System.Windows.Forms;

namespace B2BWholesale.WinForms
{
    partial class MainAdminForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControlMain = new TabControl();
            this.tabItems = new TabPage();
            this.tabAgents = new TabPage();
            this.tabOrders = new TabPage();
            this.tabDashboard = new TabPage();

            // Tab 1: Items
            this.pnlItemTop = new Panel();
            this.lblItemName = new Label();
            this.txtItemName = new TextBox();
            this.lblItemSize = new Label();
            this.txtItemSize = new TextBox();
            this.lblItemPrice = new Label();
            this.numItemPrice = new NumericUpDown();
            this.lblItemStock = new Label();
            this.numItemStock = new NumericUpDown();
            this.btnAddItem = new Button();
            this.btnUpdateItem = new Button();
            this.btnDeleteItem = new Button();
            this.dgvItems = new DataGridView();

            // Tab 2: Agents
            this.pnlAgentTop = new Panel();
            this.lblAgentName = new Label();
            this.txtAgentName = new TextBox();
            this.lblAgentPhone = new Label();
            this.txtAgentPhone = new TextBox();
            this.lblAgentAddress = new Label();
            this.txtAgentAddress = new TextBox();
            this.btnAddAgent = new Button();
            this.btnUpdateAgent = new Button();
            this.btnDeleteAgent = new Button();
            this.dgvAgents = new DataGridView();

            // Tab 3: Orders
            this.pnlOrderTop = new Panel();
            this.lblOrderStatus = new Label();
            this.cboOrderStatus = new ComboBox();
            this.btnUpdateStatus = new Button();
            this.btnPrintManifest = new Button();
            this.dgvOrders = new DataGridView();

            // Tab 4: Dashboard
            this.pnlDashTop = new Panel();
            this.lblThreshold = new Label();
            this.numStockThreshold = new NumericUpDown();
            this.lblFilterAgent = new Label();
            this.cboFilterAgent = new ComboBox();
            this.btnRefreshDashboard = new Button();
            this.tlpDashboard = new TableLayoutPanel();
            this.grpBestSelling = new GroupBox();
            this.dgvBestSelling = new DataGridView();
            this.grpLowStock = new GroupBox();
            this.dgvLowStock = new DataGridView();
            this.grpRevenue = new GroupBox();
            this.dgvRevenueByAgent = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.numItemPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numItemStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStockThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBestSelling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenueByAgent)).BeginInit();
            this.SuspendLayout();

            // ==================== TAB CONTROL ====================
            this.tabControlMain.Dock = DockStyle.Fill;
            this.tabControlMain.Font = new Font("Segoe UI", 9.5F);
            this.tabControlMain.Controls.Add(this.tabItems);
            this.tabControlMain.Controls.Add(this.tabAgents);
            this.tabControlMain.Controls.Add(this.tabOrders);
            this.tabControlMain.Controls.Add(this.tabDashboard);

            // ==================== TAB 1: QUẢN LÝ HÀNG HÓA ====================
            this.tabItems.Text = "Quản lý Hàng hóa (Items)";
            this.tabItems.Padding = new Padding(8);

            this.pnlItemTop.Dock = DockStyle.Top;
            this.pnlItemTop.Height = 135;

            this.lblItemName.Text = "Tên mặt hàng:";
            this.lblItemName.Location = new Point(12, 16);
            this.lblItemName.AutoSize = true;

            this.txtItemName.Location = new Point(120, 13);
            this.txtItemName.Size = new Size(300, 27);

            this.lblItemSize.Text = "Quy cách (Size):";
            this.lblItemSize.Location = new Point(445, 16);
            this.lblItemSize.AutoSize = true;

            this.txtItemSize.Location = new Point(560, 13);
            this.txtItemSize.Size = new Size(220, 27);

            this.lblItemPrice.Text = "Đơn giá (VNĐ):";
            this.lblItemPrice.Location = new Point(12, 54);
            this.lblItemPrice.AutoSize = true;

            this.numItemPrice.Location = new Point(120, 51);
            this.numItemPrice.Size = new Size(160, 27);
            this.numItemPrice.Maximum = 1000000000M;
            this.numItemPrice.Increment = 1000M;
            this.numItemPrice.ThousandsSeparator = true;

            this.lblItemStock.Text = "Số lượng tồn kho:";
            this.lblItemStock.Location = new Point(310, 54);
            this.lblItemStock.AutoSize = true;

            this.numItemStock.Location = new Point(445, 51);
            this.numItemStock.Size = new Size(120, 27);
            this.numItemStock.Maximum = 1000000M;

            this.btnAddItem.Text = "Thêm mặt hàng";
            this.btnAddItem.Location = new Point(120, 92);
            this.btnAddItem.Size = new Size(130, 32);
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);

            this.btnUpdateItem.Text = "Cập nhật / Tồn kho";
            this.btnUpdateItem.Location = new Point(260, 92);
            this.btnUpdateItem.Size = new Size(150, 32);
            this.btnUpdateItem.Click += new System.EventHandler(this.btnUpdateItem_Click);

            this.btnDeleteItem.Text = "Xóa mặt hàng";
            this.btnDeleteItem.Location = new Point(420, 92);
            this.btnDeleteItem.Size = new Size(130, 32);
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);

            this.pnlItemTop.Controls.AddRange(new Control[] {
                this.lblItemName, this.txtItemName, this.lblItemSize, this.txtItemSize,
                this.lblItemPrice, this.numItemPrice, this.lblItemStock, this.numItemStock,
                this.btnAddItem, this.btnUpdateItem, this.btnDeleteItem
            });

            this.dgvItems.Dock = DockStyle.Fill;
            this.dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.ReadOnly = true;
            this.dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.MultiSelect = false;
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.CellClick += new DataGridViewCellEventHandler(this.dgvItems_CellClick);

            this.tabItems.Controls.Add(this.dgvItems);
            this.tabItems.Controls.Add(this.pnlItemTop);

            // ==================== TAB 2: QUẢN LÝ ĐẠI LÝ ====================
            this.tabAgents.Text = "Quản lý Đại lý (Agents)";
            this.tabAgents.Padding = new Padding(8);

            this.pnlAgentTop.Dock = DockStyle.Top;
            this.pnlAgentTop.Height = 135;

            this.lblAgentName.Text = "Tên đại lý:";
            this.lblAgentName.Location = new Point(12, 16);
            this.lblAgentName.AutoSize = true;

            this.txtAgentName.Location = new Point(120, 13);
            this.txtAgentName.Size = new Size(340, 27);

            this.lblAgentPhone.Text = "Số điện thoại:";
            this.lblAgentPhone.Location = new Point(485, 16);
            this.lblAgentPhone.AutoSize = true;

            this.txtAgentPhone.Location = new Point(585, 13);
            this.txtAgentPhone.Size = new Size(195, 27);

            this.lblAgentAddress.Text = "Địa chỉ:";
            this.lblAgentAddress.Location = new Point(12, 54);
            this.lblAgentAddress.AutoSize = true;

            this.txtAgentAddress.Location = new Point(120, 51);
            this.txtAgentAddress.Size = new Size(660, 27);

            this.btnAddAgent.Text = "Thêm đại lý";
            this.btnAddAgent.Location = new Point(120, 92);
            this.btnAddAgent.Size = new Size(130, 32);
            this.btnAddAgent.Click += new System.EventHandler(this.btnAddAgent_Click);

            this.btnUpdateAgent.Text = "Cập nhật đại lý";
            this.btnUpdateAgent.Location = new Point(260, 92);
            this.btnUpdateAgent.Size = new Size(140, 32);
            this.btnUpdateAgent.Click += new System.EventHandler(this.btnUpdateAgent_Click);

            this.btnDeleteAgent.Text = "Xóa đại lý";
            this.btnDeleteAgent.Location = new Point(410, 92);
            this.btnDeleteAgent.Size = new Size(130, 32);
            this.btnDeleteAgent.Click += new System.EventHandler(this.btnDeleteAgent_Click);

            this.pnlAgentTop.Controls.AddRange(new Control[] {
                this.lblAgentName, this.txtAgentName, this.lblAgentPhone, this.txtAgentPhone,
                this.lblAgentAddress, this.txtAgentAddress,
                this.btnAddAgent, this.btnUpdateAgent, this.btnDeleteAgent
            });

            this.dgvAgents.Dock = DockStyle.Fill;
            this.dgvAgents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAgents.ReadOnly = true;
            this.dgvAgents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAgents.MultiSelect = false;
            this.dgvAgents.AllowUserToAddRows = false;
            this.dgvAgents.CellClick += new DataGridViewCellEventHandler(this.dgvAgents_CellClick);

            this.tabAgents.Controls.Add(this.dgvAgents);
            this.tabAgents.Controls.Add(this.pnlAgentTop);

            // ==================== TAB 3: QUẢN LÝ ĐƠN HÀNG ====================
            this.tabOrders.Text = "Quản lý Đơn hàng & Shipping Manifest";
            this.tabOrders.Padding = new Padding(8);

            this.pnlOrderTop.Dock = DockStyle.Top;
            this.pnlOrderTop.Height = 60;

            this.lblOrderStatus.Text = "Trạng thái mới:";
            this.lblOrderStatus.Location = new Point(12, 18);
            this.lblOrderStatus.AutoSize = true;

            this.cboOrderStatus.Location = new Point(125, 14);
            this.cboOrderStatus.Size = new Size(160, 28);
            this.cboOrderStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboOrderStatus.Items.AddRange(new object[] { "Pending", "Approved", "Shipped", "Cancelled" });

            this.btnUpdateStatus.Text = "Cập nhật trạng thái";
            this.btnUpdateStatus.Location = new Point(300, 12);
            this.btnUpdateStatus.Size = new Size(160, 32);
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);

            this.btnPrintManifest.Text = "In Shipping Manifest";
            this.btnPrintManifest.Location = new Point(475, 12);
            this.btnPrintManifest.Size = new Size(185, 32);
            this.btnPrintManifest.Click += new System.EventHandler(this.btnPrintManifest_Click);

            this.pnlOrderTop.Controls.AddRange(new Control[] {
                this.lblOrderStatus, this.cboOrderStatus, this.btnUpdateStatus, this.btnPrintManifest
            });

            this.dgvOrders.Dock = DockStyle.Fill;
            this.dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.MultiSelect = false;
            this.dgvOrders.AllowUserToAddRows = false;

            this.tabOrders.Controls.Add(this.dgvOrders);
            this.tabOrders.Controls.Add(this.pnlOrderTop);

            // ==================== TAB 4: DASHBOARD & FILTER ====================
            this.tabDashboard.Text = "Dashboard & Thống kê";
            this.tabDashboard.Padding = new Padding(8);

            this.pnlDashTop.Dock = DockStyle.Top;
            this.pnlDashTop.Height = 58;

            this.lblThreshold.Text = "Cảnh báo tồn kho <=";
            this.lblThreshold.Location = new Point(12, 18);
            this.lblThreshold.AutoSize = true;

            this.numStockThreshold.Location = new Point(160, 14);
            this.numStockThreshold.Size = new Size(80, 27);
            this.numStockThreshold.Maximum = 10000M;
            this.numStockThreshold.Value = 10M;

            this.lblFilterAgent.Text = "Lọc theo đại lý:";
            this.lblFilterAgent.Location = new Point(265, 18);
            this.lblFilterAgent.AutoSize = true;

            this.cboFilterAgent.Location = new Point(375, 14);
            this.cboFilterAgent.Size = new Size(280, 28);
            this.cboFilterAgent.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnRefreshDashboard.Text = "Lọc / Làm mới";
            this.btnRefreshDashboard.Location = new Point(675, 12);
            this.btnRefreshDashboard.Size = new Size(140, 32);
            this.btnRefreshDashboard.Click += new System.EventHandler(this.btnRefreshDashboard_Click);

            this.pnlDashTop.Controls.AddRange(new Control[] {
                this.lblThreshold, this.numStockThreshold, this.lblFilterAgent,
                this.cboFilterAgent, this.btnRefreshDashboard
            });

            this.tlpDashboard.Dock = DockStyle.Fill;
            this.tlpDashboard.ColumnCount = 2;
            this.tlpDashboard.RowCount = 2;
            this.tlpDashboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tlpDashboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            this.grpBestSelling.Text = "Top Mặt hàng Bán chạy nhất (Best-Selling Items)";
            this.grpBestSelling.Dock = DockStyle.Fill;
            this.dgvBestSelling.Dock = DockStyle.Fill;
            this.dgvBestSelling.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBestSelling.ReadOnly = true;
            this.dgvBestSelling.AllowUserToAddRows = false;
            this.grpBestSelling.Controls.Add(this.dgvBestSelling);

            this.grpLowStock.Text = "Cảnh báo Sắp hết hàng (Low-Stock Warning)";
            this.grpLowStock.Dock = DockStyle.Fill;
            this.dgvLowStock.Dock = DockStyle.Fill;
            this.dgvLowStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLowStock.ReadOnly = true;
            this.dgvLowStock.AllowUserToAddRows = false;
            this.grpLowStock.Controls.Add(this.dgvLowStock);

            this.grpRevenue.Text = "Tổng Doanh thu theo Đại lý (Total Revenue by Agent)";
            this.grpRevenue.Dock = DockStyle.Fill;
            this.dgvRevenueByAgent.Dock = DockStyle.Fill;
            this.dgvRevenueByAgent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRevenueByAgent.ReadOnly = true;
            this.dgvRevenueByAgent.AllowUserToAddRows = false;
            this.grpRevenue.Controls.Add(this.dgvRevenueByAgent);

            this.tlpDashboard.Controls.Add(this.grpBestSelling, 0, 0);
            this.tlpDashboard.Controls.Add(this.grpLowStock, 1, 0);
            this.tlpDashboard.Controls.Add(this.grpRevenue, 0, 1);
            this.tlpDashboard.SetColumnSpan(this.grpRevenue, 2);

            this.tabDashboard.Controls.Add(this.tlpDashboard);
            this.tabDashboard.Controls.Add(this.pnlDashTop);

            // ==================== MAIN FORM ====================
            this.ClientSize = new Size(1120, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "B2B Wholesale Management - Back-Office Admin";
            this.Controls.Add(this.tabControlMain);

            ((System.ComponentModel.ISupportInitialize)(this.numItemPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numItemStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStockThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBestSelling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenueByAgent)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private TabControl tabControlMain;
        private TabPage tabItems;
        private TabPage tabAgents;
        private TabPage tabOrders;
        private TabPage tabDashboard;

        private Panel pnlItemTop;
        private Label lblItemName;
        private TextBox txtItemName;
        private Label lblItemSize;
        private TextBox txtItemSize;
        private Label lblItemPrice;
        private NumericUpDown numItemPrice;
        private Label lblItemStock;
        private NumericUpDown numItemStock;
        private Button btnAddItem;
        private Button btnUpdateItem;
        private Button btnDeleteItem;
        private DataGridView dgvItems;

        private Panel pnlAgentTop;
        private Label lblAgentName;
        private TextBox txtAgentName;
        private Label lblAgentPhone;
        private TextBox txtAgentPhone;
        private Label lblAgentAddress;
        private TextBox txtAgentAddress;
        private Button btnAddAgent;
        private Button btnUpdateAgent;
        private Button btnDeleteAgent;
        private DataGridView dgvAgents;

        private Panel pnlOrderTop;
        private Label lblOrderStatus;
        private ComboBox cboOrderStatus;
        private Button btnUpdateStatus;
        private Button btnPrintManifest;
        private DataGridView dgvOrders;

        private Panel pnlDashTop;
        private Label lblThreshold;
        private NumericUpDown numStockThreshold;
        private Label lblFilterAgent;
        private ComboBox cboFilterAgent;
        private Button btnRefreshDashboard;
        private TableLayoutPanel tlpDashboard;
        private GroupBox grpBestSelling;
        private DataGridView dgvBestSelling;
        private GroupBox grpLowStock;
        private DataGridView dgvLowStock;
        private GroupBox grpRevenue;
        private DataGridView dgvRevenueByAgent;
    }
}