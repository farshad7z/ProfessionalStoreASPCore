using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models
{
   public class Product
    {
        [Key] // تعریف کلید اولیه
        public int ProductId { get; set; } // شناسه محصول

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // نام محصول

        public decimal Price { get; set; } // قیمت محصول

        // دیگر ویژگی‌های مدل محصول
    }
}
