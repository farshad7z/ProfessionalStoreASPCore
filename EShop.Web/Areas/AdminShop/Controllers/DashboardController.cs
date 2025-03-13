using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.AdminShop.Controllers
{
    [Area("AdminShop")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
