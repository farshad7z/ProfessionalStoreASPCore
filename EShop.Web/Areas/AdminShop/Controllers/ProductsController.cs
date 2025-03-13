using EShop.BLL.Services;
using EShop.Core.DTOs.ViewModels.Admin.Product;
using EShop.Core.DTOs.ViewModels.Product;
using EShop.Core.Entities.Models;
using EShop.Core.Interfaces.Services.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Web.Areas.AdminShop.Controllers
{
    [Area("AdminShop")]
    [Authorize(AuthenticationSchemes ="UserAuth")]
    public class ProductsController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly IProductCategoryServices _productCategoryServices; // برای کار با دسته‌ها
        private readonly IWebHostEnvironment _env;

        // مجوزهای فایل مجاز و محدودیت حجم (مثلاً 10 مگابایت)
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly long _fileSizeLimit = 10 * 1024 * 1024; // 10MB

        public ProductsController(IProductServices productServices , IProductCategoryServices productCategoryServices, IWebHostEnvironment env)
        {
            _productServices = productServices;
            _productCategoryServices = productCategoryServices;
            _env = env;

        }

        // GET: ProductsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.CategoryList = new MultiSelectList(await _productCategoryServices.GetAllAsync(), "CategoryId", "Name");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(AdminProductCreateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.CategoryList = new MultiSelectList(await _productCategoryServices.GetAllAsync(), "CategoryId", "Name");
                    return View(model);
                }

                // اعتبارسنجی فایل آپلود شده جهت جلوگیری از آپلود فایل‌های مخرب
                if (!IsValidImage(model.Image))
                {
                    ModelState.AddModelError("Image", "فایل آپلود شده معتبر نیست یا ممکن است مخرب باشد.");
                    ViewBag.CategoryList = new MultiSelectList(await _productCategoryServices.GetAllAsync(), "CategoryId", "Name");
                    return View(model);
                }

                // مسیر پوشه آپلودها
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "products");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                // تولید نام یکتا برای فایل تصویر
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // ذخیره فایل اصلی
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                // تولید Thumbnail (مثلاً ابعاد 200x200 پیکسل)
                string thumbFileName = "thumb_" + uniqueFileName;
                string thumbPath = Path.Combine(uploadsFolder, thumbFileName);
                await CreateThumbnailAsync(filePath, thumbPath, 200, 200);

                // ایجاد شیء محصول
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    FullDescription = model.FullDescription,
                    Price = model.Price,
                    DiscountedPrice = model.DiscountedPrice,
                    ImageName = uniqueFileName, // ذخیره نام تصویر اصلی
                    ShopId = model.ShopId,
                    IsAvailable = model.IsAvailable,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow
                };

                // افزودن محصول به دیتابیس
                await _productServices.AddAsync(product);

                // ذخیره ارتباط محصول با دسته‌ها (فرض کنید رابطه چند به چند یا جدول واسط وجود دارد)
                if (model.SelectedCategoryIds != null && model.SelectedCategoryIds.Any())
                {
                    foreach (var catId in model.SelectedCategoryIds)
                    {
                        await _productServices.AddProductCategoryAsync(new ProductSelectCategory { ProductId = product.Id, ProductCategoryId = catId });
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }


        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController/Edit/5
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
        #region Functions


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

        // متد غیرهمزمان برای ساخت Thumbnail
        private Task CreateThumbnailAsync(string sourcePath, string targetPath, int width, int height)
        {
            return Task.Run(() =>
            {
                using (var image = Image.FromFile(sourcePath))
                {
                    var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
                    thumb.Save(targetPath, ImageFormat.Jpeg);
                }
            });
        }


        #endregion
    }
}
