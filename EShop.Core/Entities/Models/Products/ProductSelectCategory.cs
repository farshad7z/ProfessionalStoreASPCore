using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models
{
    public class ProductSelectCategory
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int ProductCategoryId { get; set; }
        public bool IsMainCategory { get; set; } = false; // آیا این دسته‌بندی اصلی است؟

        #region Relations
        public Product? Product { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        #endregion
    }

}
