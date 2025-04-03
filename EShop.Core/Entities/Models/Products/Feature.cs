using EShop.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models.Products
{
    public class Feature
    {
        public int Id { get; set; }

        [Display(Name ="نام ویژگی")]
        [MaxLength(100)]
        public required string Name { get; set; } // مثال: "رنگ"، "حافظه"

        [Display(Name = "نوع")]
                public FeatureType Type { get; set; } // enum: Text, Number, Range

        #region Relation
        public virtual ICollection<ProductFeatureValue>? ProductFeatureValue { get; set; }
        public virtual ICollection<CategoryFeatureValue>? CategoryFeatureValue { get; set; }

        #endregion
    }
}
