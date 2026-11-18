using B2BWholesale.Core.Data;
using B2BWholesale.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace B2BWholesale.Core.Services
{
    public record OrderLineInput(int ItemID, int Quantity);

    public class AuthService
    {
        private readonly B2BContext _db;
        public AuthService(B2BContext db) => _db = db;

        public async Task<(bool Success, string Message, AppUser? User)> AuthenticateAsync(
            string username, string password, string requiredRole)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return (false, "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.", null);

            var user = await _db.Users.Include(u => u.Agent)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null || user.password != password)
                return (false, "Tên đăng nhập hoặc mật khẩu không chính xác.", null);

            if (user.IsLocked)
                return (false, "Tài khoản này đã bị khóa (Locked). Vui lòng liên hệ quản trị viên.", null);

            if (!string.Equals(user.Role, requiredRole, StringComparison.OrdinalIgnoreCase))
                return (false, $"Truy cập bị từ chối: Chỉ tài khoản có quyền '{requiredRole}' mới được đăng nhập.", null);

            return (true, "Đăng nhập thành công.", user);
        }
    }
}