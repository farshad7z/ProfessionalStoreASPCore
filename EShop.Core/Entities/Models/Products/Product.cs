using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public required string ImageName { get; set; } 

        public bool IsAvailable { get; set; } = true; // وضعیت موجودی
        public bool IsDeleted { get; set; } = false;

        public int ShopId { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        #region Relations
        public Shop? Shop { get; set; } // فروشگاه مرتبط
        public ProductSEO? ProductSEO { get; set; }
        // Other Relationships
        //public ICollection<ProductGallery> Galleries { get; set; } // تصاویر محصول
        //public ICollection<ProductFeature> Features { get; set; } // ویژگی‌ها
        //public ICollection<ProductComment> Comments { get; set; } // نظرات

        public ICollection<ProductSelectCategory>? ProductSelectCategory { get; set; } // گروه‌های انتخابی
        #endregion
    }
}
