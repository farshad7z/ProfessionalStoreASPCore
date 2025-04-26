using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using EShop.Core.Enums;
using EShop.Core.Entities.Models.Products;

namespace EShop.Core.Entities.Models
{
    public class ProductCategory
    {
        public int CategoryId { get; set; } // شناسه دسته‌بندی

        [StringLength(100, ErrorMessage = "نام دسته‌بندی نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public required string Name { get; set; }

        [StringLength(350, ErrorMessage = "توضیحات نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string? Description { get; set; } // توضیحات دسته‌بندی

        public int? ParentId { get; set; } // دسته‌بندی والد
        [MaxLength(250)]
        public string? ImageName { get; set; } 

        [MaxLength(200, ErrorMessage = "نوع منو نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public MenuType MenuType { get; set; } = MenuType.CategoryOnly; // محل نمایش در منو

        [StringLength(50, ErrorMessage = "کلاس آیکون نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string? IconClass { get; set; } // کلاس آیکون دسته‌بندی (برای UI)
        [StringLength(50, ErrorMessage = "بوم رنگی  نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string? IconColor { get; set; } // _ آیکون دسته‌بندی (برای رنگ

        [StringLength(150, ErrorMessage = "Slug نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string? Slug { get; set; } // آدرس سئو شده دسته‌بندی (اختیاری)

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // تاریخ ایجاد
        public DateTime? UpdatedAt { get; set; } // آخرین بروزرسانی

        public bool IsCategoryOnMain { get; set; } = false;

        public bool IsDeleted { get; set; } = false;


        #region Relations
        public virtual ProductCategory? Parent { get; set; }
        public virtual ICollection<ProductCategory> Children { get; set; } = new HashSet<ProductCategory>();
        public virtual ICollection<ProductSelectCategory>? ProductSelectCategory { get; set; } // گروه‌های انتخابی
        public virtual ICollection<CategoryFeature>? CategoryFeatureValue { get; set; }


        #endregion
    }
}
