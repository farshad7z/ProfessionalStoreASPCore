using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models.Products
{
    public class ProductVariant
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [MaxLength(250)]
        public required string VariantName { get; set; } // نام ترکیب (مثلاً "آبی - 54 - ابریشم - خارجی")

        public decimal Price { get; set; } // قیمت این ترکیب

        public int StockQuantity { get; set; } // موجودی این ترکیب

        public bool IsAvailable { get; set; } = true; // وضعیت فعال بودن

        #region Relations

        public Product Product { get; set; }
        public ICollection<ProductVariantFeature> VariantFeatures { get; set; } = new List<ProductVariantFeature>(); // ویژگی‌های این ترکیب
        #endregion
    }

}
