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

        #region Relations
        public Product? Product { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        #endregion
    }

}
