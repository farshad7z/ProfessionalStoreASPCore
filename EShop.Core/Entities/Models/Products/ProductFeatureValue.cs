using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models.Products
{
    public class ProductFeatureValue
    {
        public int Id { get; set; }

        public int ProductFeatureId { get; set; } // ارتباط با ویژگی

        [MaxLength(100)]
        public required string Value { get; set; } // مقدار ویژگی (مثلاً "آبی" یا "XL")
        #region Relations
        public ProductFeature ProductFeature { get; set; }
        #endregion
    }

}
