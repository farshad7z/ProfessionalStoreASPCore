using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models
{
    public class Shop
    {
        public int ShopId { get; set; } 
        public required  string ShopName { get; set; } 
        public required string Address { get; set; }
        public required string PostalCode { get; set; } 
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; } 
        public required string Country { get; set; } 
        public required string City { get; set; } 
        public required string State { get; set; } 
        public required int OwnerId { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; } // تاریخ آخرین به‌روزرسانی

        // ایجاد رابطه با کاربر صاحب فروشگاه
        public required User Owner { get; set; }
    }

}
