using EShop.Infrastructure.Convertors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Generator
{
    public static class NameGenerator
    {


        /// <summary>
        /// تولید نام فایل تصویر مناسب برای سئو، با استفاده از نام محصول و پسوند فایل.
        /// نام تولیدشده شامل معادل فارسی و انگلیسی نام محصول + GUID برای یکتا بودن است.
        /// </summary>
        /// <param name="productName">نام محصول</param>
        /// <param name="extension">پسوند فایل (مثلاً .jpg, .png)</param>
        /// <returns>نام فایل مناسب برای SEO</returns>
       public static string GenerateImageName(string productName, string extension)
        {
            if (string.IsNullOrWhiteSpace(productName))
                return $"{Guid.NewGuid()}{extension}";

            string seoName = productName.Replace(" ", "-").ToLower();
            string latinName = StringConvertor.PersianToLatinOrEnglish(productName).Replace(" ", "-").ToLower();

            string imageName = $"عکس-تصویر-{seoName}-{latinName}-{Guid.NewGuid()}{extension}";
            return System.Text.RegularExpressions.Regex.Replace(imageName, "-{2,}", "-"); // حذف --- اضافی
        }
        /// <summary>
        /// تولید نام slug  مناسب برای سئو، با استفاده از نام فارسی و تبدیل به ان به معادل لاتین  .
        /// نام تولیدشده شامل معادل فارسی و انگلیسی نام محصول
        /// </summary>
        /// <param name="name">نام </param>
        /// <returns>نام فایل مناسب برای SEO</returns>
        public static string GenerateSlugName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return $"{Guid.NewGuid()}";

            string replaceName = name.Replace(" ", "-").ToLower();
            string latinName = StringConvertor.PersianToLatinOrEnglish(name).Replace(" ", "-").ToLower();

            string seoName = $"{replaceName}-{latinName}";
            return System.Text.RegularExpressions.Regex.Replace(seoName, "-{2,}", "-"); // حذف --- اضافی
        }

    }
}
