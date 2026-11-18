using B2BWholesale.Core.Services;
using B2BWholesale.WinForms; // Chứa DbFactory
using System;
using System.Windows.Forms;

namespace B2BWholesale.WinForms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // Vô hiệu hóa nút trong lúc xử lý để tránh click nhiều lần
            btnLogin.Enabled = false;

            try
            {
                using var db = DbFactory.CreateContext();
                var authService = new AuthService(db);

                var result = await authService.AuthenticateAsync(txtUsername.Text, txtPassword.Text, "Admin");

                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ẩn form đăng nhập và mở form chính
                    this.Hide();
                    var mainForm = new MainAdminForm();
                    mainForm.FormClosed += (s, args) => this.Close(); // Đóng hẳn app khi tắt mainForm
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show(result.Message, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }
    }
}