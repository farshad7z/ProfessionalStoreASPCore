using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models.Products
{
    public class ProductVariantFeature
    {
        public int Id { get; set; }

        public int ProductVariantId { get; set; }

        public int ProductFeatureValueId { get; set; }

        #region Relations

        public ProductFeature ProductFeatureValue { get; set; }  
        public ProductVariant ProductVariant { get; set; }
        #endregion
    }

}
