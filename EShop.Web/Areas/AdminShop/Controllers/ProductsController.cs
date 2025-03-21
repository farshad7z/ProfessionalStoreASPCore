using EShop.BLL.Services;
using EShop.BLL.Services.Public;
using EShop.Core.DTOs.ViewModels.Admin.Product;
using EShop.Core.DTOs.ViewModels.Product;
using EShop.Core.Entities.Models;
using EShop.Core.Interfaces.Services;
using EShop.Core.Interfaces.Services.Public;
using EShop.DAL.Migrations;
using EShop.Infrastructure.Convertors;
using EShop.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IProductServices _productServices;
        private readonly IProductCategoryServices _productCategoryServices; // برای کار با دسته‌ها
        private readonly IAccountServices _AccountServices;
        private readonly IProductGalleryServices _productGalleryServices;
        private readonly IProductSEOService _productSEOService;

        private readonly IWebHostEnvironment _env;

        // مجوزهای فایل مجاز و محدودیت حجم (مثلاً 10 مگابایت)
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly long _fileSizeLimit = 10 * 1024 * 1024; // 10MB


        public ProductsController(
              IProductServices productServices
            , IProductCategoryServices productCategoryServices
            , IAccountServices AccountServices
            , IProductGalleryServices productGalleryServices
            , IProductSEOService productSEOService
            , IWebHostEnvironment env)
        {
            _AccountServices = AccountServices;
            _productServices = productServices;
            _productCategoryServices = productCategoryServices;
            _productGalleryServices = productGalleryServices;
            _productSEOService = productSEOService;
            _env = env;

        }


        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            var allProducts = await _productServices.GetAllProductsWithCategoriesAsync();


            ViewBag.ImagePath = "~/uploads/products/thumbnail";
            return View(allProducts);
        }




        #region Product
        // GET: ProductsController/Create
        public async Task<IActionResult> Create()
        {
            AdminProductCreateViewModel model = new AdminProductCreateViewModel();
            var categories = await _productCategoryServices.GetAllAsync();

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
                    ViewBag.CategoryList = new MultiSelectList(await _productCategoryServices.GetAllAsync(), "CategoryId", "Name");
                    return View(model);
                }

                // استخراج فرمت تصویر از فایل ورودی
                var extension = Path.GetExtension(model.ImageProduct.FileName);
                if (string.IsNullOrEmpty(extension))
                {
                    ModelState.AddModelError("ImageProduct", "فرمت تصویر شناخته نشده است.");
                    ViewBag.CategoryList = new MultiSelectList(await _productCategoryServices.GetAllAsync(), "CategoryId", "Name");
                    return View(model);
                }



                // تولید نام یکتا برای تصویر
                string imageName = GenerateImageName(model.Name, extension);

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

                bool exists = await _AccountServices.IsExistsEmployeeAndHasShopByUserIdAsync(userIdInt);
                if (!exists)
                {
                    ModelState.AddModelError("PublicError", "شما در هیچ فروشگاهی کارمند نیستید.");
                    return View(model);
                }

                var employeeDetails = await _AccountServices.GetDetailsEmployeeByUserIdAsync(userIdInt);
                int shopId = employeeDetails.ShopId ?? 0;
                if (model.DiscountedPrice > model.Price)
                {
                    ViewBag.CategoryList = new MultiSelectList(await _productCategoryServices.GetAllAsync(), "CategoryId", "Name");
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
                await _productServices.AddAsync(product);

                // ذخیره ارتباط محصول با دسته‌ها
                if (model.SelectedCategoryIds != null && model.SelectedCategoryIds.Any())
                {
                    foreach (var catId in model.SelectedCategoryIds)
                    {
                        await _productServices.AddProductCategoryAsync(new ProductSelectCategory { ProductId = product.Id, ProductCategoryId = catId });
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
            var product = await _productServices.GetByIdAsync(id);
            if (product == null) return NotFound();


            var selectedCategories = await _productServices.GetProductSelectCategoriesByIdAsync(id);
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


            var categories = await _productCategoryServices.GetAllAsync();

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

                var product = await _productServices.GetByIdAsync(model.ProductId);
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
                await _productServices.UpdateProductSelectCategoriesAsync(model.ProductId, model.SelectedCategoryIds);

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
                        string imageName = GenerateImageName(model.Name, extension);

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

                await _productServices.UpdateAsync(product);
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


        #region Functions

        /// <summary>
        /// تولید نام فایل تصویر مناسب برای سئو، با استفاده از نام محصول و پسوند فایل.
        /// نام تولیدشده شامل معادل فارسی و انگلیسی نام محصول + GUID برای یکتا بودن است.
        /// </summary>
        /// <param name="productName">نام محصول</param>
        /// <param name="extension">پسوند فایل (مثلاً .jpg, .png)</param>
        /// <returns>نام فایل مناسب برای SEO</returns>
        string GenerateImageName(string productName, string extension)
        {
            if (string.IsNullOrWhiteSpace(productName))
                return $"{Guid.NewGuid()}{extension}";

            string seoName = productName.Replace(" ", "-").ToLower();
            string latinName = StringConvertor.PersianToLatinMap(productName).Replace(" ", "-").ToLower();

            string imageName = $"عکس-تصویر-{seoName}-{latinName}-{Guid.NewGuid()}{extension}";
            return System.Text.RegularExpressions.Regex.Replace(imageName, "-{2,}", "-"); // حذف --- اضافی
        }



        // متد برای ساخت Thumbnail
        private bool IsValidImage(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !_permittedExtensions.Contains(ext))
                return false;

            if (file.Length == 0 || file.Length > _fileSizeLimit)
                return false;

            // بررسی امضای باینری فایل (Magic Numbers)
            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                var signatures = GetImageSignatures(ext);
                var headerBytes = reader.ReadBytes(signatures.Max(s => s.Length));
                return signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
            }
        }

        private List<byte[]> GetImageSignatures(string extension)
        {
            var signatures = new List<byte[]>();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    signatures.Add(new byte[] { 0xFF, 0xD8, 0xFF });
                    break;
                case ".png":
                    signatures.Add(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A });
                    break;
                case ".gif":
                    signatures.Add(Encoding.ASCII.GetBytes("GIF87a"));
                    signatures.Add(Encoding.ASCII.GetBytes("GIF89a"));
                    break;
            }
            return signatures;
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

                bool exists = await _AccountServices.IsExistsEmployeeAndHasShopByUserIdAsync(userIdInt);
                if (!exists)
                {
                    ModelState.AddModelError("PublicError", "شما در هیچ فروشگاهی کارمند نیستید.");
                    return View(model);
                }

                // تولید نام یکتا برای تصویر
                string? imageName = model.Title != null
                    ? $"{"عکس-تصویر-گالری"}-{model.Title.Replace(" ", "-").ToLower()}-{StringConvertor.PersianToLatinMap(model.Title).Replace(" ", "-").ToLower()}-{Guid.NewGuid().ToString()}{extension}"
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

                await _productGalleryServices.AddAsync(gallery);


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
            var gallery = await _productGalleryServices.GetGalleryByIdAsync(id);
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
                await _productGalleryServices.DeleleAsync(gallery);

            }



            return RedirectToAction("Galleries", new { id = gallery.ProductId });


        }

        public async Task<ActionResult> ListGalleryProduct(int productId)
        {
            var listGallery = await _productGalleryServices.GetGalleriesForProductAsync(productId);
            return PartialView("_ListGalleryProduct", listGallery);
        }

        #endregion
        //--** End Gallery Cods **--


        //--** Start SEO Cods **--
        #region SEO


        public async Task<ActionResult> SEO(int productId,string? productName)
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
                return RedirectToAction("Index","Products");
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

    }
}
