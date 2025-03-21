using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.DTOs.ViewModels.Product
{
    public class ProductGalleryViewModel
    {
        [Display(Name = "عنوان")]
        public required string Title { get; set; }

        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
    }
}
