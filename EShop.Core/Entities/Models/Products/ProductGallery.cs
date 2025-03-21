using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models
{
    public class ProductGallery
    {
        public int GalleryId { get; set; }

        [Display(Name = "کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public required int ProductId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public required string Title { get; set; }

        [Display(Name = "تصویر")]
        public  string ImageName { get; set; }

        #region Relation
        public Product? Product { get; set; }
        #endregion


    }
}
