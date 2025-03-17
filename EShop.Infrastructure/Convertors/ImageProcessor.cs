using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace EShop.Infrastructure.Convertors
{
    public static class ImageProcessor
    {
        /// <summary>
        /// تولید یک تصویر بندانگشتی (Thumbnail) از فایل ورودی.
        /// </summary>
        /// <param name="file">فایل تصویر ورودی</param>
        /// <param name="thumbnailPath">مسیر ذخیره‌سازی تصویر بندانگشتی</param>
        /// <param name="width">عرض بندانگشتی (پیش‌فرض: 150 پیکسل)</param>
        /// <param name="height">ارتفاع بندانگشتی (پیش‌فرض: 150 پیکسل)</param>
        public static void CreateThumbnail(IFormFile file, string thumbnailPath, int width = 150, int height = 150)
        {
            using var image = Image.Load(file.OpenReadStream());

            // تغییر اندازه تصویر به سایز بندانگشتی
            image.Mutate(x => x.Resize(width, height));

            // ذخیره تصویر بندانگشتی در مسیر مشخص‌شده
            image.Save(thumbnailPath, new JpegEncoder { Quality = 85 });
        }




        /// <summary>
        /// بهینه‌سازی تصویر اصلی برای کاهش حجم بدون افت کیفیت زیاد.
        /// </summary>
        /// <param name="file">فایل تصویر ورودی</param>
        /// <param name="outputPath">مسیر ذخیره‌سازی تصویر بهینه‌شده</param>
        /// <param name="maxFileSizeKB">حداکثر حجم مجاز تصویر به کیلوبایت (پیش‌فرض: 500 کیلوبایت)</param>
        /// <param name="quality">درصد کیفیت تصویر (پیش‌فرض: 90%)</param>
        public static void OptimizeImage(IFormFile file, string outputPath, int maxFileSizeKB = 500, int quality = 90)
        {
            using var image = Image.Load(file.OpenReadStream());

            // تعیین مسیر موقت برای ذخیره تصویر فشرده‌شده
            string tempPath = outputPath + "_temp.jpg";

            // ذخیره تصویر با کیفیت اولیه
            image.Save(tempPath, new JpegEncoder { Quality = quality });

            // بررسی حجم فایل
            FileInfo fileInfo = new FileInfo(tempPath);
            while (fileInfo.Length / 1024 > maxFileSizeKB && quality > 50)
            {
                quality -= 5; // کاهش کیفیت به صورت مرحله‌ای
                image.Save(tempPath, new JpegEncoder { Quality = quality });
                fileInfo = new FileInfo(tempPath); // حجم جدید را بررسی کن
            }

            // اگر هنوز حجم تصویر زیاد بود، اندازه را کاهش بده
            while (fileInfo.Length / 1024 > maxFileSizeKB)
            {
                image.Mutate(x => x.Resize((int)(image.Width * 0.9), (int)(image.Height * 0.9)));
                image.Save(tempPath, new JpegEncoder { Quality = quality });
                fileInfo = new FileInfo(tempPath);
            }

            // جایگزینی فایل اصلی با فایل بهینه‌شده
            File.Move(tempPath, outputPath, true);
        }





        ///// <summary>
        ///// بهینه‌سازی تصویر اصلی برای کاهش حجم بدون افت کیفیت زیاد.
        ///// </summary>
        ///// <param name="file">فایل تصویر ورودی</param>
        ///// <param name="outputPath">مسیر ذخیره‌سازی تصویر بهینه‌شده</param>
        ///// <param name="quality">درصد کیفیت تصویر (پیش‌فرض: 80%)</param>
        //public static void OptimizeImage(IFormFile file, string outputPath, int quality = 80)
        //{
        //    using var image = Image.Load(file.OpenReadStream());

        //    // ذخیره تصویر بهینه‌شده با کیفیت مشخص‌شده
        //    image.Save(outputPath, new JpegEncoder { Quality = quality });
        //}
    }
}
