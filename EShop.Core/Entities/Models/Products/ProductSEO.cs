using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models
{
    public class ProductSEO
    {
        public int Id { get; set; } // Primary Key
        public int ProductId { get; set; } // Foreign Key to Product

        [MaxLength(150)]
        public required string Slug { get; set; } // URL-friendly slug

        [MaxLength(250)]
        public string? MetaTitle { get; set; } // عنوان متا

        [MaxLength(300)]
        public string? MetaDescription { get; set; } // توضیحات متا

        [MaxLength(150)]
        public string? MetaKeywords { get; set; } // کلمات کلیدی متا

        #region Relations
        public Product? Product { get; set; } // ارتباط با محصول
        #endregion
    }


}
