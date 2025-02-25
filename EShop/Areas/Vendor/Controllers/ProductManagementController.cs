using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Vendor.Controllers
{
    public class ProductManagementController : Controller
    {
        // GET: ProductManagementController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductManagementController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductManagementController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductManagementController/Create
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

        // GET: ProductManagementController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductManagementController/Edit/5
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

        // GET: ProductManagementController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductManagementController/Delete/5
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
