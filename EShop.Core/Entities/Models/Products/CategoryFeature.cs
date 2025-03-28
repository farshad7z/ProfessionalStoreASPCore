using EShop.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models.Products
{

    public class CategoryFeature
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; } // مثال: "رنگ"، "حافظه"

        public FeatureType Type { get; set; } // enum: Text, Number, Range

        public int CategoryId { get; set; }

        #region Relation
        public ProductCategory Category { get; set; }
        public virtual ICollection<ProductFeatureValue>? ProductFeatureValue { get; set; }

        #endregion
    }
}
