using B2BWholesale.Core.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B2BWholesale.Web.Controllers
{
    [Authorize(Roles = "Agent")]
    public class CatalogController : Controller
    {
        private readonly B2BContext _db;
        public CatalogController(B2BContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var items = await _db.Items.OrderBy(i => i.ItemID).ToListAsync();
            return View(items);
        }
    }
}