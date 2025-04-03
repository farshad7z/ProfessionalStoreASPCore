using EShop.Core.Entities.Models;
using EShop.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Core.DTOs.ViewModels.Admin.Category
{
    public class AdminCreateCategoryViewModel
    {
        [Required(ErrorMessage = "نام دسته‌بندی الزامی است.")]
        [StringLength(100, ErrorMessage = "نام دسته‌بندی نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        [Display(Name = "نام دسته‌بندی")]
        public  string Name { get; set; }

        [StringLength(350, ErrorMessage = "توضیحات نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "دسته‌بندی والد")]
        public int? ParentId { get; set; } // دسته‌بندی والد

        [Display(Name = "تصویر")]
        public IFormFile? ImageFile { get; set; } // فایل تصویر برای آپلود

        [Display(Name = "نوع منو")]
        public MenuType MenuType { get; set; } = MenuType.CategoryOnly; // محل نمایش در منو

        [StringLength(50, ErrorMessage = "کلاس آیکون نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        [Display(Name = "کلاس آیکون")]
        public string? IconClass { get; set; }

        [StringLength(50, ErrorMessage = "بوم رنگی نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        [Display(Name = "رنگ آیکون")]
        public string? IconColor { get; set; }

        [Display(Name = "Slug")]
        public string? Slug { get; set; }

        [Display(Name = "نمایش در صفحه اصلی")]
        public bool IsCategoryOnMain { get; set; } = false; // آیا در صفحه اصلی نمایش داده شود؟

        [Display(Name = "دسته‌بندی‌های والد")]
        public List<SelectListItem> ParentCategories { get; set; } = new List<SelectListItem>();
    }
}
// لیستی از دسته‌بندی‌های والد برای انتخاب
