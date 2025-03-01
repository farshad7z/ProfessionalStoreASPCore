using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models
{
    public class Vendor
    {
        public int VendorId { get; set; } // شناسه فروشنده
        public string UserId { get; set; } // شناسه کاربری که به فروشنده تعلق دارد
        public string ShopName { get; set; } // نام فروشگاه
        public string ShopDescription { get; set; } // توضیحات فروشگاه
        public bool IsActive { get; set; } // وضعیت فروشگاه (فعال/غیرفعال)
        public string Address { get; set; } // آدرس فروشگاه
        public string? PhoneNumber { get; set; } // شماره تماس
        public DateTime CreatedAt { get; set; } // تاریخ ایجاد فروشگاه
        public DateTime? UpdatedAt { get; set; } // تاریخ آخرین به‌روزرسانی
        public string? UserRoles { get; set; } // نقش‌های مختلف در فروشگاه (مثل مدیر فروشگاه، کارمند فروشگاه)

        public User? User { get; set; } // رابطه با کاربر (User)
    }

}
