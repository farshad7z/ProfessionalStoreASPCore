using EShop.Infrastructure.Convertors;
using EShop.Infrastructure.Generator;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

namespace EShop.Web.Areas.AdminShop.Controllers
{
    [Area("AdminShop")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long MAX_FILE_SIZE = 2 * 1024 * 1024; // 2MB

        public UploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        
          
          
            [HttpPost("image")]
            public async Task<IActionResult> ImageUpload(IFormFile file, string? productName)
            {
                if (file == null || file.Length == 0)
                return BadRequest("فایلی برای آپلود ارسال نشده است.");

            // اعتبارسنجی فرمت فایل
            if (!IsValidImage(file))
                return BadRequest("فرمت فایل نامعتبر است. فقط فرمت‌های JPG, PNG, GIF, WEBP مجاز هستند.");

            // اعتبارسنجی MIME Type
            if (!IsValidMimeType(file))
                return BadRequest("نوع فایل نامعتبر است!");

            // بررسی حجم فایل
            if (file.Length > MAX_FILE_SIZE)
                return BadRequest("حجم فایل بیش از حد مجاز است (حداکثر 2MB).");

            // مسیر ذخیره‌سازی
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "ckeditor");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var extension = Path.GetExtension(file.FileName);

            // تولید نام یکتا برای تصویر
            string? safeFileName = NameGenerator.GenerateImageName(productName, extension);
               
            // تغییر نام فایل (جلوگیری از حملات)
            var filePath = Path.Combine(uploadPath, safeFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // ارسال مسیر فایل آپلود شده به CKEditor
            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/ckeditor/{safeFileName}";
            return Ok(new { url = fileUrl });
        }

        // متد بررسی فرمت فایل
        private bool IsValidImage(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            return _allowedExtensions.Contains(extension);
        }

        // متد بررسی نوع MIME
        private bool IsValidMimeType(IFormFile file)
        {
            var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
            return allowedMimeTypes.Contains(file.ContentType.ToLower());
        }
    }
}

