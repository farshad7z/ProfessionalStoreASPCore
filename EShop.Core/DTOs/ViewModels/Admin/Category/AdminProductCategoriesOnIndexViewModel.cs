using EShop.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.DTOs.ViewModels.Admin.Category
{
    public class AdminProductCategoriesOnIndexViewModel
    {
        public int CategoryId { get; set; } // شناسه دسته‌بندی

        public required string Name { get; set; }

        public string? Description { get; set; } // توضیحات دسته‌بندی

        public Dictionary<int , string>? ParentId { get; set; } // دسته‌بندی والد
        public List<ProductCategoriesChildViewModel>? child { get; set; } // دسته‌بندی والد

        public MenuType MenuType { get; set; } = MenuType.CategoryOnly; // محل نمایش در منو

        public string? Image { get; set; } // کلاس آیکون دسته‌بندی (برای UI)

        public string? Slug { get; set; } // آدرس سئو شده دسته‌بندی (اختیاری)

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // تاریخ ایجاد
        public DateTime? UpdatedAt { get; set; } // آخرین بروزرسانی

        public bool IsCategoryOnMain { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

    }

    public class ProductCategoriesChildViewModel
    {
       public int? Id { get; set; }
       public string? Name { get; set; }
    }

}
