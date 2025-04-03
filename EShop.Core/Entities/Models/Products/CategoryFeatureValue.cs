using EShop.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models.Products
{

    public class CategoryFeatureValue
    {
        public int Id { get; set; }

        public int FeatureId { get; set; } // ارتباط با ویژگی
        public int CategoryId { get; set; } // ارتباط با دسته محصولات
        
        //[MaxLength(100)]
        //public required string Value { get; set; } // مقدار ویژگی (مثلاً "آبی" یا "XL")

        #region Relation
        public ProductCategory Category { get; set; }
        public  Feature Feature { get; set; }
        #endregion
    }
}
