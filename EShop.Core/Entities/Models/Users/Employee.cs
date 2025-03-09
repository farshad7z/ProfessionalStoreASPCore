using EShop.Core.Entities.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.Entities.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; } // شناسه کارمندs

        [MaxLength(300)]
        public required string? Address { get; set; } // آدرس
        [MaxLength(12)]
        public string? SecondaryPhoneNumber { get; set; } // شماره تماس دوم
        [MaxLength(10)]
        public string? NationalCode { get; set; } // کد ملی
        [StringLength(34)]
        public string? BankAccountIBAN { get; set; }
        [MaxLength(300)]
        public string? PasswordHash { get; set; }
        public bool IsActive { get; set; } = true; // وضعیت فعال بودن
        public bool IsDeleted { get; set; }


        public int? ShopId { get; set; } // اگر کارمند فروشگاه باشد، شناسه فروشگاه را دارد
        public int UserId { get; set; } // شناسه کاربر اصلی

        #region Relation
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        #endregion


        #region Relations
        public User? User { get; set; } // کاربر
        public Shop? Shop { get; set; } // فروشگاه
        #endregion
    }


}

