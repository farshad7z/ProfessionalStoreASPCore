using EShop.BLL.Services;
using EShop.BLL.Services.Public;
using EShop.Core.DTOs.ViewModels.Admin.Product;
using EShop.Core.DTOs.ViewModels.Product;
using EShop.Core.Entities.Enums;
using EShop.Core.Entities.Models;
using EShop.Core.Entities.Models.Products;
using EShop.Core.Interfaces.Services;
using EShop.Core.Interfaces.Services.Public;
using EShop.Core.ViewModels.Common;
using EShop.DAL.Migrations;
using EShop.Infrastructure.Convertors;
using EShop.Infrastructure.Generator;
using EShop.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Packaging.Signing;
using SixLabors.ImageSharp;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Web.Areas.AdminShop.Controllers
{
    [Area("AdminShop")]
    [Authorize, Authorize(AuthenticationSchemes = "AdminAuth")]
    public class ProductsController : Controller
    {
        private readonly IProductServices _productService;
        private readonly IProductCategoryServices _productCategoryService; // برای کار با دسته‌ها
        private readonly IAccountServices _AccountService;
        private readonly IProductGalleryServices _productGalleryService;
        private readonly IProductSEOService _productSEOService;
        private readonly IFeatureService _featureService;
        private readonly IVariantServices _productVariantService;
        private readonly IPublicServices _publicService;

        private readonly IWebHostEnvironment _env;

        // مجوزهای فایل مجاز و محدودیت حجم (مثلاً 10 مگابایت)
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly long _fileSizeLimit = 10 * 1024 * 1024; // 10MB


        public ProductsController(
              IProductServices productService
            , IProductCategoryServices productCategoryService
            , IAccountServices AccountService
            , IProductGalleryServices productGalleryService
            , IProductSEOService productSEOService
            , IFeatureService featureService
            , IVariantServices productVariantService
            , IPublicServices publicService
            , IWebHostEnvironment env)
        {
            _AccountService = AccountService;
            _productService = productService;
            _productCategoryService = productCategoryService;
            _productGalleryService = productGalleryService;
            _productSEOService = productSEOService;
            _featureService = featureService;
            _productVariantService = productVariantService;
            _publicService = publicService;
            _env = env;

        }


        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            var allProducts = await _productService.GetAllProductsWithCategoriesAsync();


            ViewBag.ImagePath = "~/uploads/products/thumbnail";
            return View(allProducts);
        }




        #region Product
        // GET: ProductsController/Create
        public async Task<IActionResult> Create()
        {
            AdminProductCreateViewModel model = new AdminProductCreateViewModel();
            var categories = await _productCategoryService.GetAllAsync();

            // تبدیل دسته‌بندی‌ها به ساختار درختی
            var categoryTree = BuildCategoryTree(categories, null);
            model.Categories = categoryTree;
            TempData["Categories"] = JsonConvert.SerializeObject(categoryTree);

            return View(model);
        }

        // متد بازگشتی برای ساخت درخت دسته‌بندی‌ها
        private List<CategoryViewModel> BuildCategoryTree(IEnumerable<ProductCategory> categories, int? parentId)
        {
            return categories
                .Where(c => c.ParentId == parentId)
                .Select(c => new CategoryViewModel
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    SubCategories = BuildCategoryTree(categories, c.CategoryId) // بازگشتی
                }).ToList();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(AdminProductCreateViewModel model)
        {
            var categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(TempData["Categories"].ToString());
            model.Categories = categories;
            TempData.Keep("Categories");

            // اعتبارسنجی تصویر ارسالی
            if (!ImageValidator.IsValidImage(model.ImageProduct))
            {
                ModelState.AddModelError("ImageProduct", "فرمت فایل معتبر نیست یا فایل مخرب است.");
                return View(model);
            }



            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.CategoryList = new MultiSelectList(await _productCategoryService.GetAllAsync(), "CategoryId", "Name");
                    return View(model);
                }

                // استخراج فرمت تصویر از فایل ورودی
                var extension = Path.GetExtension(model.ImageProduct.FileName);
                if (string.IsNullOrEmpty(extension))
                {
                    ModelState.AddModelError("ImageProduct", "فرمت تصویر شناخته نشده است.");
                    ViewBag.CategoryList = new MultiSelectList(await _productCategoryService.GetAllAsync(), "CategoryId", "Name");
                    return View(model);
                }



                // تولید نام یکتا برای تصویر
                string imageName = NameGenerator.GenerateImageName(model.Name, extension);

                //string? imageName = model.Name != null
                //    ? $"{"عکس-تصویر"}-{model.Name.Replace(" ", "-").ToLower()}-{StringConvertor.PersianToLatinMap(model.Name).Replace(" ", "-").ToLower()}-{Guid.NewGuid().ToString()}{extension}"
                //    : $"{Guid.NewGuid().ToString()}{extension}";
                //imageName = System.Text.RegularExpressions.Regex.Replace(imageName, "-{2,}", "-");



                string thumbImageName = "thumbnail_" + imageName;

                // مسیر پوشه آپلودها
                string mainImagePath = Path.Combine(_env.WebRootPath, "uploads", "products");
                string thumbnailPath = Path.Combine(_env.WebRootPath, "uploads", "products", "thumbnail");

                Directory.CreateDirectory(mainImagePath);
                Directory.CreateDirectory(thumbnailPath);

                mainImagePath = Path.Combine(mainImagePath, imageName);
                thumbnailPath = Path.Combine(thumbnailPath, thumbImageName);

                // دریافت شناسه کاربری از User.Identity
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null || !int.TryParse(userId, out int userIdInt))
                {
                    ModelState.AddModelError("PublicError", "کاربر وارد شده غیر مجاز است.");
                    return View(model);
                }

                bool exists = await _AccountService.IsExistsEmployeeAndHasShopByUserIdAsync(userIdInt);
                if (!exists)
                {
                    ModelState.AddModelError("PublicError", "شما در هیچ فروشگاهی کارمند نیستید.");
                    return View(model);
                }

                var employeeDetails = await _AccountService.GetDetailsEmployeeByUserIdAsync(userIdInt);
                int shopId = employeeDetails.ShopId ?? 0;
                if (model.DiscountedPrice > model.Price)
                {
                    ViewBag.CategoryList = new MultiSelectList(await _productCategoryService.GetAllAsync(), "CategoryId", "Name");
                    ModelState.AddModelError("DiscountedPrice", "قیمت تخفیف نباید بیشتر از قیمت اصلی کالا باشد.");
                    return View(model);
                }
                // ایجاد شیء محصول
                var product = new Product
                {
                    Name = System.Text.RegularExpressions.Regex.Replace(model.Name, "' '{2,}", " "),
                    Description = model.Description,
                    FullDescription = model.FullDescription,
                    Price = model.Price,
                    DiscountedPrice = model.DiscountedPrice,
                    ProductImageName = imageName, // ذخیره نام تصویر اصلی
                    ShopId = shopId,
                    IsAvailable = model.IsAvailable,
                    IsPublished = model.IsPublished,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow
                };

                // افزودن محصول به دیتابیس
                await _productService.AddAsync(product);

                // ذخیره ارتباط محصول با دسته‌ها
                if (model.SelectedCategoryIds != null && model.SelectedCategoryIds.Any())
                {
                    foreach (var catId in model.SelectedCategoryIds)
                    {
                        await _productService.AddProductCategoryAsync(new ProductSelectCategory { ProductId = product.Id, ProductCategoryId = catId, IsMainCategory = catId == model.MainCategoryId });
                    }
                }

                // ذخیره و بهینه‌سازی تصویر اصلی
                ImageProcessor.OptimizeImage(model.ImageProduct, mainImagePath);

                // ایجاد و ذخیره تصویر بندانگشتی
                ImageProcessor.CreateThumbnail(model.ImageProduct, thumbnailPath);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // در صورت بروز خطا، پیام خطا را لاگ کنید
                //_logger.LogError(ex, "Error in creating product.");
                return View(model);
            }
        }

        // GET: ProductsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();


            var selectedCategories = await _productService.GetProductSelectCategoriesByIdAsync(id);
            List<int> selectedCategoryIds = selectedCategories?.ToList() ?? new List<int>();

            var model = new AdminProductEditViewModel
            {
                ProductId = product.Id,
                Name = System.Text.RegularExpressions.Regex.Replace(product.Name, "' '{2,}", " "),
                Description = product.Description,
                Price = product.Price,
                DiscountedPrice = product.DiscountedPrice,
                FullDescription = product.FullDescription,
                IsAvailable = product.IsAvailable,
                IsPublished = product.IsPublished,
                ImageUrl = "~/uploads/products/thumbnail/thumbnail_" + product.ProductImageName,
                SelectedCategoryIds = selectedCategoryIds,

            };


            var categories = await _productCategoryService.GetAllAsync();

            // تبدیل دسته‌بندی‌ها به ساختار درختی
            var categoryTree = BuildCategoryTree(categories, null);
            // ذخیره دستهبندیها در TempData
            TempData["Categories"] = JsonConvert.SerializeObject(categoryTree);

            model.Categories = categoryTree;



            return View(model);
        }



        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminProductEditViewModel model)
        {
            var categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(TempData["Categories"].ToString());
            model.Categories = categories;
            TempData.Keep("Categories");

            // اعتبارسنجی تصویر ارسالی
            if (model.ImageProduct != null && !ImageValidator.IsValidImage(model.ImageProduct))
            {
                ModelState.AddModelError("ImageProduct", "فرمت فایل معتبر نیست یا فایل مخرب است.");
                return View(model);
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (model.DiscountedPrice > model.Price)
                {
                    ModelState.AddModelError("DiscountedPrice", "قیمت تخفیف نباید بیشتر از قیمت اصلی کالا باشد.");
                    return View(model);
                }

                var product = await _productService.GetByIdAsync(model.ProductId);
                if (product == null) return NotFound();

                product.Name = model.Name;
                product.Description = model.Description;
                product.FullDescription = model.FullDescription;
                product.Price = model.Price;
                product.DiscountedPrice = model.DiscountedPrice;
                product.IsAvailable = model.IsAvailable;
                product.IsPublished = model.IsPublished;
                product.UpdatedAt = DateTime.Now;

                // حذف دسته‌بندی‌های قبلی و افزودن دسته‌های جدید
                await _productService.UpdateProductSelectCategoriesAsync(model.ProductId, model.SelectedCategoryIds);

                // اگر تصویر جدیدی آپلود شده باشد
                if (model.ImageProduct != null && model.ImageProduct.FileName != product.ProductImageName)
                {
                    if (model.ImageProduct.FileName != "default_product.png")
                    {

                        // استخراج فرمت تصویر از فایل ورودی
                        var extension = Path.GetExtension(model.ImageProduct.FileName);
                        if (string.IsNullOrEmpty(extension))
                        {
                            ModelState.AddModelError("ImageProduct", "فرمت تصویر شناخته نشده است.");
                            return View(model);
                        }


                        // تولید نام یکتا برای تصویر
                        string imageName = NameGenerator.GenerateImageName(model.Name, extension);

                        string thumbImageName = "thumbnail_" + imageName;

                        // مسیر پوشه آپلودها
                        string mainImagePath = Path.Combine(_env.WebRootPath, "uploads", "products");
                        string thumbnailPath = Path.Combine(_env.WebRootPath, "uploads", "products", "thumbnail");

                        //عکس های قبلی

                        if (!string.IsNullOrEmpty(product.ProductImageName))
                        {
                            string previousMainImagePath = Path.Combine(mainImagePath, product.ProductImageName);
                            string previousthumbnailPath = Path.Combine(thumbnailPath, "thumbnail_" + product.ProductImageName);

                            if (System.IO.File.Exists(previousMainImagePath)) System.IO.File.Delete(previousMainImagePath);
                            if (System.IO.File.Exists(previousthumbnailPath)) System.IO.File.Delete(previousthumbnailPath);
                        }

                        Directory.CreateDirectory(mainImagePath);
                        Directory.CreateDirectory(thumbnailPath);

                        mainImagePath = Path.Combine(mainImagePath, imageName);
                        thumbnailPath = Path.Combine(thumbnailPath, thumbImageName);



                        // ذخیره و بهینه‌سازی تصویر اصلی
                        ImageProcessor.OptimizeImage(model.ImageProduct, mainImagePath);

                        // ایجاد و ذخیره تصویر بندانگشتی
                        ImageProcessor.CreateThumbnail(model.ImageProduct, thumbnailPath);




                        product.ProductImageName = imageName;

                    }


                }

                await _productService.UpdateAsync(product);
                return RedirectToAction("Index");
            }
            catch (IOException ex)
            {
                ModelState.AddModelError("", "خطا در آپلود یا حذف تصویر.");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "خطایی رخ داده است. لطفاً دوباره تلاش کنید.");
                return View(model);
            }

        }


        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
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
        #endregion


        //--** Start Gallery Cods **--
        #region Gallery


        public ActionResult Galleries(int Id)
        {
            // ViewBag.Galleries = db.Product_Galleries.Where(g => g.ProductID == id).ToList();
            return View(new AdminProductGalleryViewModel()
            {
                ProductId = Id,
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Galleries(AdminProductGalleryViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {

                if (!ImageValidator.IsValidImage(model.ImageFile))
                {
                    ModelState.AddModelError("ImageFile", "فرمت فایل معتبر نیست یا فایل مخرب است.");
                    return View(model);
                }
                // استخراج فرمت تصویر از فایل ورودی
                var extension = Path.GetExtension(model.ImageFile.FileName);
                if (string.IsNullOrEmpty(extension))
                {
                    ModelState.AddModelError("ImageFile", "فرمت تصویر شناخته نشده است.");
                    return View(model);
                }

                // دریافت شناسه کاربری از User.Identity
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null || !int.TryParse(userId, out int userIdInt))
                {
                    ModelState.AddModelError("PublicError", "کاربر وارد شده غیر مجاز است.");
                    return View(model);
                }

                bool exists = await _AccountService.IsExistsEmployeeAndHasShopByUserIdAsync(userIdInt);
                if (!exists)
                {
                    ModelState.AddModelError("PublicError", "شما در هیچ فروشگاهی کارمند نیستید.");
                    return View(model);
                }

                // تولید نام یکتا برای تصویر
                string? imageName = model.Title != null
                    ? $"{"عکس-تصویر-گالری"}-{model.Title.Replace(" ", "-").ToLower()}-{StringConvertor.PersianToLatinOrEnglish(model.Title).Replace(" ", "-").ToLower()}-{Guid.NewGuid().ToString()}{extension}"
                    : $"{Guid.NewGuid().ToString()}{extension}";
                imageName = System.Text.RegularExpressions.Regex.Replace(imageName, "-{2,}", "-");


                string thumbImageName = "thumbnail_" + imageName;

                // مسیر پوشه آپلودها
                string mainImagePath = Path.Combine(_env.WebRootPath, "uploads", "ProductGalleries");
                string thumbnailPath = Path.Combine(_env.WebRootPath, "uploads", "ProductGalleries", "thumbnail");

                Directory.CreateDirectory(mainImagePath);
                Directory.CreateDirectory(thumbnailPath);

                mainImagePath = Path.Combine(mainImagePath, imageName);
                thumbnailPath = Path.Combine(thumbnailPath, thumbImageName);




                var gallery = new ProductGallery
                {
                    Title = System.Text.RegularExpressions.Regex.Replace(model.Title, "' '{2,}", " "),
                    ImageName = imageName, // ذخیره نام تصویر اصلی
                    ProductId = model.ProductId,

                };

                await _productGalleryService.AddAsync(gallery);


                // ذخیره و بهینه‌سازی تصویر اصلی
                ImageProcessor.OptimizeImage(model.ImageFile, mainImagePath);

                // ایجاد و ذخیره تصویر بندانگشتی
                ImageProcessor.CreateThumbnail(model.ImageFile, thumbnailPath);

            }
            catch (Exception ex)
            {
                // در صورت بروز خطا، پیام خطا را لاگ کنید
                //_logger.LogError(ex, "Error in creating product.");
                return View(model);
            }


            return RedirectToAction("Galleries", new { id = model.ProductId });
        }



        public async Task<ActionResult> DeleteGallery(int id)
        {
            var gallery = await _productGalleryService.GetGalleryByIdAsync(id);
            //عکس های قبلی

            if (!string.IsNullOrEmpty(gallery.ImageName))
            {
                string thumbImageName = "thumbnail_" + gallery.ImageName;

                // مسیر پوشه آپلودها
                string mainImagePath = Path.Combine(_env.WebRootPath, "uploads", "ProductGalleries");
                string thumbnailPath = Path.Combine(_env.WebRootPath, "uploads", "ProductGalleries", "thumbnail");

                mainImagePath = Path.Combine(mainImagePath, gallery.ImageName);
                thumbnailPath = Path.Combine(thumbnailPath, thumbImageName);



                if (System.IO.File.Exists(mainImagePath)) System.IO.File.Delete(mainImagePath);
                if (System.IO.File.Exists(thumbnailPath)) System.IO.File.Delete(thumbnailPath);
                await _productGalleryService.DeleleAsync(gallery);

            }



            return RedirectToAction("Galleries", new { id = gallery.ProductId });


        }

        public async Task<ActionResult> ListGalleryProduct(int productId)
        {
            var listGallery = await _productGalleryService.GetGalleriesForProductAsync(productId);
            return PartialView("_ListGalleryProduct", listGallery);
        }

        #endregion

        //--** End Gallery Cods **--


        //--** Start SEO Cods **--
        #region SEO


        public async Task<ActionResult> SEO(int productId, string? productName)
        {
            var seo = await _productSEOService.GetSEOByIdProductAsync(productId);
            ViewBag.ProductName = productName ?? "محصول";
            return View(seo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SEO(AdminProductSEOViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // ذخیره‌سازی داده‌ها در دیتابیس
                await _productSEOService.AddUpdateAsync(model);
                return RedirectToAction("Index", "Products");
            }
            catch (Exception ex)
            {
                // مدیریت خطا
                // شاید لازم باشد پیام خطا به کاربر نمایش داده شود
            }

            // پس از موفقیت، به صفحه ویرایش سئو همان محصول باز می‌گردیم
            return RedirectToAction("SEO", new { productId = model.ProductId, productName = model.MetaTitle });
        }

        #endregion
        //--** End SEO Cods **--

        //--** Start Feature And Variand Cods **--


        #region Feature Cods

        // GET: /AdminShop/Products/ManageProductFeaturesAndVariant/5
        [HttpGet]
        public async Task<IActionResult> ManageProductFeatures(int id)
        {
            ViewBag.ProductColors = EnumHelper.GetProductColorItems<ProductColor>();

            // مرحله ۱: دریافت اطلاعات محصول از دیتابیس
            var product = await _productService.GetByIdAndIncludeSelectProductCategoryAsync(id);
            if (product == null) return NotFound();

            // مرحله ۲: دریافت لیست شناسه‌های دسته‌بندی‌های این محصول
            var categoryIds = product.ProductSelectCategory
                                     .Select(pc => pc.ProductCategoryId)
                                     .ToList();

            // مرحله ۳: به صورت موازی دریافت کن:
            // - ویژگی‌های مرتبط با دسته‌بندی‌ها
            // - مقادیر ذخیره‌شده برای این محصول
            //var featuresTask = _featureService.GetListFeaturesByCategoryIdsAsync(categoryIds);
            //var existingValuesTask = _featureService.GetListFeaturesValuesByProductIdAsync(id);

            // مرحله ۴: صبر کن تا هر دو عملیات بالا همزمان تموم بشن
            //await Task.WhenAll(featuresTask, existingValuesTask);

            // مرحله ۵: استخراج نتایج از Task‌ها
            var features = await _featureService.GetListFeaturesByCategoryIdsAsync(categoryIds);
            var existingValues = await _featureService.GetListFeaturesValuesByProductIdAsync(id);



            // مرحله6: ساخت ViewModel نهایی برای نمایش در صفحه
            var vm = new AdminManageProductFeaturesViewModel
            {
                ProductId = product.Id,
                ProductName = product.Name,

                Features = features.Select(f =>
                {
                    var item = new AdminFeatureItemViewModel
                    {
                        FeatureId = f.FeatureId,
                        FeatureName = f.Feature.Name,
                        FeatureType = f.Feature.Type
                    };

                    return item;

                }).ToList(),
                SavedProductFeatures = existingValues.Select(spf =>
                {
                    var item = new AdminFeatureItemViewModel
                    {
                        FeatureId = spf.Feature.Id,
                        FeatureName = spf.Feature.Name,
                        FeatureType = spf.Feature.Type

                    };
                    if (spf.Feature.Type == FeatureType.Range && !string.IsNullOrWhiteSpace(spf.Value))
                    {
                        try
                        {
                            var parts = spf.Value.Split('-');

                            if (parts.Length == 2 &&
                                decimal.TryParse(parts[0], out var min) &&
                                decimal.TryParse(parts[1], out var max))
                            {
                                item.RangeMin = min;
                                item.RangeMax = max;
                            }
                        }
                        catch
                        {
                            // در صورت خطا، مقدار پیشفرض یا خالی تنظیم شود
                            item.RangeMin = null;
                            item.RangeMax = null;
                        }
                    }
                    else
                    {
                        item.Value = spf.Value;
                    }
                    return item;

                }).ToList(),
            };

            return View(vm);
        }


        // POST: ذخیره ویژگی‌ها
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductFeature([FromBody] AddFeaturViewModel model)
        {
            // بررسی اولیه ورودی‌ها
            if (string.IsNullOrWhiteSpace(model.Value) || model.ProductId <= 0 || model.FeatureId <= 0)
                return Json(ResponseModel<bool>.Fail("داده‌های ورودی نامعتبر است", 400));

            // بررسی وجود محصول
            var product = await _productService.GetByIdAsync(model.ProductId);
            if (product == null)
                return Json(ResponseModel<bool>.Fail("محصول مورد نظر یافت نشد", 404));

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || !int.TryParse(userId, out int userIdInt))
            {
                return Json(ResponseModel<bool>.Fail("کاربر وارد شده غیر مجاز است.", 404));
            }

            // بررسی دسترسی کاربر به محصول
            var accessResult = await _publicService.IsAccessUserToThisProductAsync(userIdInt, model.ProductId, product.ShopId ?? 0);
            if (!accessResult.IsSuccess || !accessResult.Data)
                return Json(ResponseModel<bool>.Fail(accessResult.Message, 403));

            // افزودن ویژگی به محصول (اینجا باید متد واقعی خودتو صدا بزنی)
            var result = await _productService.AddFeatureToProductAsync(model.ProductId, model.FeatureId, model.Value);
            if (!result.IsSuccess)
            {
                return Json(ResponseModel<bool>.Fail(result.Message, result.StatusCode));
            }

            return Json(ResponseModel<bool>.Success(true, result.Message));

        }

        #endregion
        //--** End Feature Cods **--

        //--** Start Variand Cods **--
        #region Variant Cods


        [HttpGet]
        public async Task<IActionResult> ManageProductVariants(int id)
        {
            // مرحله ۱: دریافت اطلاعات محصول
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                TempData["error_message"] = "محصول مورد نظر یافت نشد.";
                return NotFound();
            }

            // مرحله ۲: دریافت ویژگی‌های مرتبط با دسته‌بندی اصلی و مقادیرشان برای این محصول
            var featureValues = await _featureService.GetFeatureValuesOfMainCategoryByProductIdAsync(id);
            if (featureValues == null || !featureValues.Any())
            {
                TempData["error_message"] = "هیچ ویژگی مرتبط با دسته‌بندی اصلی برای این محصول یافت نشد.";
                return NotFound();
            }

            // مرحله ۳: فیلتر ویژگی‌هایی که برای واریانت تعریف شده‌اند (IsVariant)
            var variantFeatures = featureValues
                .Where(f => f.Feature != null
                && f.Feature.CategoryFeature != null
                && f.Feature.CategoryFeature.Any(cf => cf.IsVariant))
                .Select(f => new AdminFeatureItemViewModel
                {
                    FeatureId = f.FeatureId ?? 0,
                    FeatureValueId = f.Id,
                    FeatureName = f.Feature.Name,
                    FeatureType = f.Feature.Type,
                    Value = f.Value
                }).ToList();

            // مرحله ۴: دریافت واریانت‌های ذخیره‌شده برای این محصول
            var existingVariants = await _productVariantService.GetVariantsByProductIdAsync(id);

            var savedVariants = existingVariants.Select(v => new AdminVariantItemViewModel
            {
                VariantId = v.Id,
                VariantName = v.VariantName,
                Price = v.Price,
                StockQuantity = v.StockQuantity,
                IsAvailable = v.IsAvailable
            }).ToList();

            // مرحله ۵: ساخت ViewModel نهایی
            var vm = new AdminManageProductVariantsViewModel
            {
                ProductId = product.Id,
                ProductName = product.Name,
                FeatureValues = variantFeatures,
                SavedVariants = savedVariants
            };

            return View(vm);
        }




        // POST AJAX: افزودن واریانت جدید
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVariant([FromBody] AdminAddVariantViewModel request)
        {
            if (request.SelectedFeatureValueIds == null || !request.SelectedFeatureValueIds.Any())
                return Json(new { success = false, message = "هیچ مقدار ویژگی انتخاب نشده!" });

            var variant = new ProductVariant
            {
                ProductId = request.ProductId,
                VariantName = request.VariantName,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                IsAvailable = request.IsAvailable
            };
            await _productVariantService.AddVariantToProductAsync(variant);
            
            await _productVariantService.AddRangeFeatureToProductVariantFeatureAsync(variant.Id, request.SelectedFeatureValueIds);

            return Json(new { success = true, message = "واریانت با موفقیت اضافه شد." });
        }


        //// POST AJAX: حذف واریانت
        //[HttpPost]
        //[ValidateAntiForgeryToken]


        //{
        //    var ok = await _productVariantService.DeleteAsync(request.VariantId);
        //    if (!Ok) return Json(new { success = false, message = "حذف واریانت ناموفق بود!" });
        //    return Json(new { success = true });
        //}

        #endregion
        //--** End Variand Cods **--

    }

}

