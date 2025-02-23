using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class VendorsController : Controller
    {
        // GET: VendorsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VendorsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VendorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VendorsController/Create
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

        // GET: VendorsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VendorsController/Edit/5
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

        // GET: VendorsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VendorsController/Delete/5
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
