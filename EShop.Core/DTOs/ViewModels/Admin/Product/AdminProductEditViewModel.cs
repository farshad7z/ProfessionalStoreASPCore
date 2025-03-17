using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.DTOs.ViewModels.Admin.Product
{

    public class AdminProductEditViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "{0} الزامی است")]
        [StringLength(100, ErrorMessage = "{0} باید بین {2} تا {1} کاراکتر باشد", MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [StringLength(200, ErrorMessage = "{0} نمی‌تواند بیش از {1} کاراکتر باشد")]
        public string Description { get; set; }

        [Display(Name = "قیمت اصلی")]
        [Required(ErrorMessage = "{0} الزامی است")]
        [Range(1000, double.MaxValue, ErrorMessage = "{0} باید حداقل {1} تومان باشد")]
        public decimal Price { get; set; }

        [Display(Name = "قیمت تخفیف")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} نمی‌تواند منفی باشد")]
        public decimal? DiscountedPrice { get; set; }

        [Display(Name = "توضیحات کامل")]
        [StringLength(2000, ErrorMessage = "{0} نمی‌تواند بیش از {1} کاراکتر باشد")]
        public string FullDescription { get; set; }

        [Display(Name = "وضعیت موجودی")]
        public bool IsAvailable { get; set; }
        [Display(Name = "وضعیت انتشار")]
        public bool IsPublished { get; set; } = true; // وضعیت انتشار

        [Display(Name = "تصویر محصول")]
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "فرمت {0} باید {1} باشد")]
        //[MaxFileSize(5 * 1024 * 1024, ErrorMessage = "حجم {0} نمی‌تواند بیشتر از {1} مگابایت باشد")]
        public IFormFile? ImageProduct { get; set; }

        [Display(Name = "آدرس تصویر فعلی")]
        public string? ImageUrl { get; set; }

        [Display(Name = "دسته‌بندی‌ها")]
        [Required(ErrorMessage = "انتخاب {0} الزامی است")]
        [MinLength(1, ErrorMessage = "حداقل یک {0} باید انتخاب شود")]
        public List<int> SelectedCategoryIds { get; set; } = new();

        public List<CategoryViewModel>? Categories { get; set; }
    }
}
