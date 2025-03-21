using System.ComponentModel.DataAnnotations;

namespace EShop.Core.DTOs.ViewModels.Admin.Product
{

    public class AdminProductSEOViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "عنوان متا")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string? MetaTitle { get; set; }

        [Display(Name = "توضیحات متا")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string? MetaDescription { get; set; }

        [Display(Name = "کلمات کلیدی متا با علامت - یا کلمات را از هم جدا کنید")]
        [Required(ErrorMessage = "لطفاً یک کلمه کلیدی حداقل وارد کنید")]
        public string? MetaKeywords { get; set; }

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string? Slug { get; set; }
    }

}
