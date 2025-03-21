using System;
using System.Collections.Generic;
using System.Globalization;

namespace EShop.Core.DTOs.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

        // نمایش قیمت به فرمت مناسب
        public string PriceFormatted => Price.ToString("C0", new CultureInfo("fa-IR"));

        // دسته‌بندی‌های متعدد برای محصولات
        public List<string> CategoryNames { get; set; }

        // مشخصه‌های اضافی محصول
        public bool IsNew { get; set; }
        public bool IsBestSeller { get; set; }
        public bool IsTopFeatured { get; set; }

        public string CategoryName => string.Join(", ", CategoryNames);
    }
}
