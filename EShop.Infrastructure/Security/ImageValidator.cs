using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace EShop.Infrastructure.Security
{
    public static class ImageValidator
    {
        // لیست فرمت‌های مجاز برای آپلود تصاویر
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

        /// <summary>
        /// بررسی می‌کند که فایل یک تصویر معتبر باشد و فرمت آن جعلی نباشد.
        /// </summary>
        /// <param name="file">فایل ارسالی توسط کاربر</param>
        /// <returns>اگر فایل معتبر باشد مقدار true وگرنه false برمی‌گرداند.</returns>
        public static bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            // بررسی فرمت فایل بر اساس پسوند
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedExtensions.Contains(extension))
                return false;

            try
            {
                // تلاش برای باز کردن تصویر و بررسی فرمت واقعی آن
                using (var stream = file.OpenReadStream())
                {
                    // استفاده از Image.Load برای بارگذاری تصویر
                    var image = Image.Load(stream);

                    // اگر تصویر به درستی بارگذاری شود، پس فرمت آن معتبر است
                    return image != null;
                }
            }
            catch
            {
                return false; // اگر خطایی رخ دهد، یعنی فایل یک تصویر واقعی نیست
            }
        }
    }
}
