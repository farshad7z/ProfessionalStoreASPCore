using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.AdminShop.Controllers
{
    [Area("AdminShop")]
    [Authorize , Authorize(AuthenticationSchemes = "AdminAuth")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
