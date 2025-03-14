using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EShop.Core.DTOs.ViewModels.Product
{
    public class AdminProductCreateViewModel
    {
        [Required(ErrorMessage = "نام محصول الزامی است.")]
        [MaxLength(150, ErrorMessage = "نام محصول نمی‌تواند بیش از 150 کاراکتر باشد.")]
        [Display(Name = "نام محصول")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "توضیحات کوتاه الزامی است.")]
        [MaxLength(500, ErrorMessage = "توضیحات کوتاه نمی‌تواند بیش از 500 کاراکتر باشد.")]
        [Display(Name = "توضیحات کوتاه")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "توضیحات کامل الزامی است.")]
        [Display(Name = "توضیحات کامل")]
        public string FullDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "قیمت محصول الزامی است.")]
        [Range(0, double.MaxValue, ErrorMessage = "قیمت باید عددی مثبت باشد.")]
        [Display(Name = "قیمت اصلی")]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "قیمت تخفیف‌خورده باید عددی مثبت باشد.")]
        [Display(Name = "قیمت تخفیف‌ خورده")]
        public decimal? DiscountedPrice { get; set; }

        [Required(ErrorMessage = "تصویر محصول الزامی است.")]
        [Display(Name = "تصویر محصول")]
        public string ImageProduct { get; set; } = default!;

        [Required(ErrorMessage = "انتخاب فروشگاه الزامی است.")]
        [Display(Name = "فروشگاه")]
        public int ShopId { get; set; }

        [Display(Name = "دسته‌های محصول")]
        [Required(ErrorMessage = "حداقل یک دسته باید انتخاب شود.")]
        public int[] SelectedCategoryIds { get; set; } = Array.Empty<int>();

        [Display(Name = "وضعیت موجودی")]
        public bool IsAvailable { get; set; } = true;
    }
}
