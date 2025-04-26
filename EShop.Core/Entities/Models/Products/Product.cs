using EShop.Core.Entities.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.Entities.Models
{
    public class Product
    {
        public int Id { get; set; } // Primary Key

        [MaxLength(150)]
        public required string Name { get; set; } // نام محصول

        [MaxLength(500)]
        public required string Description { get; set; } // توضیحات کوتاه

        public required string FullDescription { get; set; } // توضیحات کامل (HTML)

        public required decimal Price { get; set; } // قیمت اصلی
        public decimal? DiscountedPrice { get; set; } // قیمت تخفیف‌خورده (اختیاری)
        public decimal FinalPrice => DiscountedPrice ?? Price; // قیمت نهایی

        public required string ProductImageName { get; set; } // نام فایل تصویر

        public bool IsAvailable { get; set; } = true; // وضعیت موجودی
        public bool IsDeleted { get; set; } = false;
        public bool IsPublished { get; set; } = true; // وضعیت انتشار

        public int? ShopId { get; set; } // کلید خارجی فروشگاه (nullable در صورت نیاز)

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        #region Relations

        public virtual ICollection<ProductFeature>? ProductFeatureValue { get; set; } // تصاویر محصول

        public Shop? Shop { get; set; } // فروشگاه مرتبط
        public ProductSEO? ProductSEO { get; set; }
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new HashSet<ProductVariant>(); // لیست ترکیب‌ها
        public virtual  ICollection<ProductGallery>? Galleries { get; set; } // تصاویر محصول

        // Other Relationships
        public virtual ICollection<ProductFeature> Features { get; set; } // ویژگی‌ها
        //public ICollection<ProductComment> Comments { get; set; } // نظرات

        public virtual ICollection<ProductSelectCategory> ProductSelectCategory { get; set; } = new HashSet<ProductSelectCategory>(); // گروه‌های انتخابی
        #endregion
    }
}
