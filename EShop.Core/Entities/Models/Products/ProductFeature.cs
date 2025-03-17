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

        [MaxLength(100)]
        public required string Name { get; set; } // نام ویژگی (مثلاً "رنگ" یا "جنس")
        #region Relations

        public ICollection<ProductFeatureValue> FeatureValues { get; set; } = new List<ProductFeatureValue>();
        #endregion
    }

}
