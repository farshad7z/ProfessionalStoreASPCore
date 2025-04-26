using EShop.Core.Interfaces.Services.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EShop.Core.DTOs.ViewModels.Admin.Category;
using Microsoft.AspNetCore.Mvc.Rendering;
using EShop.Infrastructure.Generator;
using EShop.Infrastructure.Security;
using EShop.Infrastructure.Convertors;
using EShop.Core.Entities.Models;
using Newtonsoft.Json;
using EShop.Core.DTOs.ViewModels.Admin.Product;
using EShop.BLL.Services.Public;
using System.Threading.Tasks;
using EShop.Core.Entities.Models.Products;
using System.Linq;

namespace EShop.Web.Areas.AdminShop.Controllers
{
    [Area("AdminShop")]
    [Authorize, Authorize(AuthenticationSchemes = "AdminAuth")]
    public class ProductCategoriesController : Controller
    {
        private readonly IProductCategoryServices _productCategoryService;
        private readonly IWebHostEnvironment _env;
        private readonly IFeatureService _featureService;

        public ProductCategoriesController(IProductCategoryServices productCategoryService
            , IFeatureService featureService
            , IWebHostEnvironment env)
        {
            _productCategoryService = productCategoryService;
            _featureService = featureService;
            _env = env;

        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<AdminProductCategoriesOnIndexViewModel> categories = await _productCategoryService.GetAllForIndexCategoryAsync();
            // مسیر تصویر محصول
            var imagePath = "/uploads/productCategories/thumbnail/sample-image.jpg";

            // بررسی وجود فایل تصویر
            if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath.TrimStart('/'))))
            {
                // اگر تصویر وجود ندارد، تصویر پیش‌فرض را استفاده کن
                imagePath = "/uploads/productCategories/thumbnail/default.jpg";
            }

            return View(categories);
        }


        // GET: ProductCategoriesController/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _productCategoryService.GetAllForSelectParentAsync();

            var viewModel = new AdminCreateCategoryViewModel
            {
                ParentCategories = categories
            };
            TempData["ParentCategories"] = JsonConvert.SerializeObject(categories);

            return View(viewModel);
        }

        // POST: ProductCategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCreateCategoryViewModel model)
        {
            if (TempData["ParentCategories"] is string tempDataValue)
            {
                model.ParentCategories = JsonConvert.DeserializeObject<List<SelectListItem>>(tempDataValue);
                TempData.Keep("ParentCategories");
            }
            else
            {
                model.ParentCategories = new List<SelectListItem>();
            }

            if (!ModelState.IsValid)
            {
                // اگر فرم نامعتبر است، دوباره صفحه را با پیام‌های خطا نمایش بده
                return View(model);
            }

            // اعتبارسنجی تصویر ارسالی
            if (model.ImageFile != null && !ImageValidator.IsValidImage(model.ImageFile))
            {
                ModelState.AddModelError("ImageProduct", "فرمت فایل معتبر نیست یا فایل مخرب است.");
                return View(model);
            }

            try
            {
                bool isCategoryNameExist = await _productCategoryService.IsCategoryNameExistsAsync(model.Name);

                if (isCategoryNameExist)
                {
                    ModelState.AddModelError("Name", $"{model.Name} قبلا ثبت شده است.");
                    return View(model);
                }

                // تولید نام اسلاگ اولیه بر اساس نام محصول
                string originalSlug = NameGenerator.GenerateSlugName(model.Name);
                string slug = originalSlug;

                bool isCategorySlugExist;
                int i = 1;

                // بررسی وجود اسلاگ تا زمانی که یک اسلاگ یکتا پیدا شود
                do
                {
                    isCategorySlugExist = await _productCategoryService.IsCategorySlugExistsAsync(slug);
                    if (isCategorySlugExist)
                    {
                        // اگر اسلاگ وجود داشت، یک عدد به انتهای اسلاگ اولیه اضافه می‌شود
                        slug = $"{originalSlug}-{i}";
                        i++;
                    }
                } while (isCategorySlugExist);

                // در این نقطه، slug یکتا است و می‌توانید از آن استفاده کنید.


                model.Slug = slug;
                string imageName = null;
                if (model.ImageFile != null)
                {
                    imageName = NameGenerator.GenerateImageName(model.Name, Path.GetExtension(model.ImageFile.FileName));

                    string thumbImageName = "thumbnail_" + imageName;

                    // مسیر پوشه آپلودها
                    string mainImagePath = Path.Combine(_env.WebRootPath, "uploads", "productcategories");
                    string thumbnailPath = Path.Combine(_env.WebRootPath, "uploads", "productcategories", "thumbnail");

                    Directory.CreateDirectory(mainImagePath);
                    Directory.CreateDirectory(thumbnailPath);

                    mainImagePath = Path.Combine(mainImagePath, imageName);
                    thumbnailPath = Path.Combine(thumbnailPath, thumbImageName);
                    // ذخیره و بهینه‌سازی تصویر اصلی
                    ImageProcessor.OptimizeImage(model.ImageFile, mainImagePath);

                    // ایجاد و ذخیره تصویر بندانگشتی
                    ImageProcessor.CreateThumbnail(model.ImageFile, thumbnailPath);


                }

                // ایجاد مدل دسته‌بندی برای ذخیره در دیتابیس
                var category = new ProductCategory
                {
                    Name = System.Text.RegularExpressions.Regex.Replace(model.Name, "' '{2,}", " "),
                    Description = model.Description,
                    ParentId = model.ParentId,
                    MenuType = model.MenuType,
                    IconClass = model.IconClass,
                    IconColor = model.IconColor,
                    Slug = model.Slug,
                    IsCategoryOnMain = model.IsCategoryOnMain,
                    ImageName = imageName, // نام فایل تصویر ذخیره‌شده
                    CreatedAt = DateTime.Now,
                    IsDeleted = false

                };

                // ذخیره در دیتابیس
                await _productCategoryService.AddAsync(category);

                // نمایش پیام موفقیت و بازگشت به صفحه لیست دسته‌بندی‌ها
                TempData["SuccessMessage"] = "دسته‌بندی با موفقیت اضافه شد.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // ثبت خطا برای دیباگ کردن
                TempData["ErrorMessage"] = "خطایی رخ داد. لطفاً مجدداً تلاش کنید.";
                return View(model);
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

        [HttpGet]
        // صفحه مدیریت ویژگی‌های دسته‌بندی
        public async Task<IActionResult> ManageCategoryFeatures(int id) // id = CategoryId
        {
            ProductCategory category = await _productCategoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            var model = new AdminCategoryFeatureViewModel
            {
                CategoryId = id,
                CategoryName = category.Name,
                Features = await _featureService.GetAllFeaturesAsync(),
                SelectedFeatures = await _productCategoryService.GetFeaturesByCategoryIdAsync(id)
            };

            return View(model);
        }

        // اکشن افزودن ویژگی‌ها
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFeatures([FromBody] AdminAddFeatureRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("داده‌های نامعتبر!");

            // دریافت ویژگی‌های موجود در دسته‌بندی
            var listCategoryFeatures = await _productCategoryService.GetFeaturesByCategoryIdAsync(request.CategoryId);
            // فیلتر ویژگی‌هایی که قبلاً اضافه نشده‌اند
            var newFeatures = request.FeatureIds
                .Except(listCategoryFeatures.Select(lcf => lcf.FeatureId))
                .Select(featureId => new CategoryFeature
                {
                    CategoryId = request.CategoryId,
                    FeatureId = featureId
                })
                .ToList();

            if (!newFeatures.Any())
                return Json(new { success = false, message = "تمام این ویژگی‌ها قبلاً اضافه شده‌اند!" });

            await _productCategoryService.AddFeatureInProductCategoryAsync(newFeatures);

            // در صورت نیاز، برای هر ویژگی اضافه شده می‌توانید نام ویژگی را نیز اضافه کنید.
            // فرض کنید newFeatures شامل FeatureId و نام ویژگی (Name) نیست؛ در این صورت می‌توانید اطلاعات مورد نیاز را از _featureService بگیرید.

            return Json(new { success = true, message = "ویژگی‌های جدید با موفقیت اضافه شدند!", addedFeatures = newFeatures });
        }

        // اکشن حذف ویژگی از دسته‌بندی
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFeature([FromBody] AdminDeleteFeatureRequest request)
        {
            var categoryFeatureValue = await _productCategoryService
                .GetCategoryFeatureByCategoryIdAndFeatureIdAsync(request.CategoryId, request.FeatureId);
            if (categoryFeatureValue == null)
                return Json(new { success = false, message = "این ویژگی در دسته‌بندی وجود ندارد!" });

            await _productCategoryService.RemoveFeatureFromCategoryAsync(categoryFeatureValue);
            return Json(new { success = true });
        }


        // اکشن برای به‌روزرسانی وضعیت ویژگی (اجباری یا واریانت بودن) در دسته‌بندی
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateFeatureStatus([FromBody] AdminUpdateFeatureStatusRequest request)
        {
            // اعتبارسنجی مدل ورودی
            if (!ModelState.IsValid)
                return BadRequest("داده‌های نامعتبر!");

            // دریافت ویژگی برای دسته‌بندی
            var categoryFeature = await _productCategoryService
                .GetCategoryFeatureByCategoryIdAndFeatureIdAsync(request.CategoryId, request.FeatureId);

            if (categoryFeature == null)
                return Json(new { success = false, message = "این ویژگی در دسته‌بندی وجود ندارد!" });

            try
            {
                // به‌روزرسانی وضعیت ویژگی
                categoryFeature.IsRequired = request.IsRequired;
                categoryFeature.IsVariant = request.IsVariant;

                // ذخیره‌سازی تغییرات در دیتابیس
                await _productCategoryService.UpdateCategoryFeatureAsync(categoryFeature);

                return Json(new { success = true, message = "وضعیت ویژگی با موفقیت به‌روزرسانی شد." });
            }
            catch (Exception ex)
            {
                // ثبت خطا برای دیباگ کردن
                return Json(new { success = false, message = "خطایی رخ داد. لطفاً مجدداً تلاش کنید." });
            }
        }

    }





}


