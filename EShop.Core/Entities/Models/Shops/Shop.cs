using EShop.Core.Entities.Models;
using System.ComponentModel.DataAnnotations;



namespace EShop.Core.Entities.Models
{
    public class Shop
    {

        public int ShopId { get; set; }

        [StringLength(200)]
        public required string ShopNameFa { get; set; }
        public string? ShopNameEn { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        // آدرس کامل با استاندارد بین‌المللی
        [StringLength(500)]
        public required string FullAddress { get; set; }

        [StringLength(20)]
        public required string PostalCode { get; set; }

        [Phone]
        [StringLength(20)]
        public required string PhoneNumber { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }

        // مختصات جغرافیایی
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdated { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public int OwnerId { get; set; } // شناسه صاحب فروشگاه

        #region Relations 
        public  User? User { get; set; }
        public ICollection<Employee>? Employees { get; set; } // ارتباط با کارمندان    
        public ICollection<Product>? Products { get; set; } // فرضاً اگر محصولات هم بخواهید برای فروشگاه داشته باشید
        #endregion
    }

}

