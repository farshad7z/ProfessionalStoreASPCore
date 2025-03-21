using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.DTOs.ViewModels.Admin.Product
{
    public class AdminProductGalleryViewModel
    {
        [Required(ErrorMessage = "عنوان تصویر الزامی است")]
        [Display(Name = "عنوان تصویر")]
        public string Title { get; set; }

        [Required(ErrorMessage = "تصویر الزامی است")]
        [DataType(DataType.Upload)]
        [Display(Name = "انتخاب تصویر")]
        public IFormFile? ImageFile { get; set; } 
        public int ProductId { get; set; }
    }
}
