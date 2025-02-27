using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Vendor.Controllers
{
    public class VendorDashboardController : Controller
    {
        // GET: VendorDashboardController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VendorDashboardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VendorDashboardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VendorDashboardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VendorDashboardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VendorDashboardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VendorDashboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VendorDashboardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
