using EShop.Core.Interfaces.Services.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EShop.Core.DTOs.ViewModels.Admin.Category;
using System.Threading.Tasks;

namespace EShop.Web.Areas.AdminShop.Controllers
{
    [Area("AdminShop")]
    [Authorize,Authorize(AuthenticationSchemes = "AdminAuth")]
    public class ProductCategoriesController : Controller
    {
        private readonly IProductCategoryServices _productCategoryServices;
        public ProductCategoriesController(IProductCategoryServices productCategoryServices)
        {
            _productCategoryServices = productCategoryServices;
        }
        public async Task<ActionResult> Index()
        {
            IEnumerable<AdminProductCategoriesOnIndexViewModel> categories =await _productCategoryServices.GetAllForIndexCategoryAsync();

            return View(categories);
        }

        // GET: ProductCategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductCategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCategoriesController/Create
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

        // GET: ProductCategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductCategoriesController/Edit/5
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

        // GET: ProductCategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductCategoriesController/Delete/5
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
