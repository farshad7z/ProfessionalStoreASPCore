using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models.Products
{
    public class ProductFeature
    {
        public int Id { get; set; }

        public int? FeatureId { get; set; } //
        public int? ProductId { get; set; } // ارتباط با محصول

        [MaxLength(100)]
        public required string Value { get; set; } // مقدار ویژگی (مثلاً "آبی" یا "XL")

        #region Relations

        public virtual Feature? Feature { get; set; }
        public virtual Product Product { get; set; }
        #endregion

        
    }

}
