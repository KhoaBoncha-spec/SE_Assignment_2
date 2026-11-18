using System.Security.Claims;
using B2BWholesale.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace B2BWholesale.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        public AccountController(AuthService authService) => _authService = authService;

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true && User.IsInRole("Agent"))
                return RedirectToAction("Index", "Catalog");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _authService.AuthenticateAsync(username, password, "Agent");
            if (!result.Success || result.User == null)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.User.UserName),
                new Claim(ClaimTypes.Role, result.User.Role),
                new Claim("AgentID", (result.User.AgentID ?? 1).ToString()),
                new Claim("AgentName", result.User.Agent?.AgentName ?? result.User.UserName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Catalog");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}