using System;
using System.Collections.Generic;
using System.Globalization;

namespace EShop.Core.DTOs.ViewModels.Product
{
    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } // توضیحات محصول
        public string ImageUrl { get; set; } // تصویر اصلی محصول
        public decimal Price { get; set; }

        // قیمت به فرمت ریالی
        public string PriceFormatted => Price.ToString("C0", new CultureInfo("fa-IR"));

        // لیست دسته‌بندی‌ها
        public List<string> CategoryNames { get; set; } = new List<string>();

        // لیست تصاویر گالری محصول
        public List<ProductGalleryViewModel> GalleryImages { get; set; } = new List<ProductGalleryViewModel>();

        // ویژگی‌های خاص محصول
        public bool IsNew { get; set; }
        public bool IsBestSeller { get; set; }
        public bool IsTopFeatured { get; set; }

        // نمایش دسته‌بندی‌ها به‌صورت رشته
        public string CategoryName => string.Join(", ", CategoryNames);
        // نمایش دکلمات کلیدی
        public string? MetaKeywords { get; set; }

    }
}
