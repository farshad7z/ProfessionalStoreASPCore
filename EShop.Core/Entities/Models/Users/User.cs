
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.Entities.Models
{
    public class User
    {

        public int UserId { get; set; }
        [MaxLength(300)]
        public string? UserName { get; set; }
        [MaxLength(300)]
        public string? FirstName { get; set; }
        [MaxLength(300)]
        public string? LastName { get; set; }
        [MaxLength(300)]
        public string? Email { get; set; }
        [MaxLength(300)]
        public string? PasswordHash { get; set; }
        [MaxLength(12)]
        public required string PhoneNumber { get; set; }

        public string? ActiveCode { get; set; }
        public bool IsActive{ get; set; }
        public bool IsEmployeeShop { get; set; }
        public bool IsEmployeeSite { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginDate { get; set; }

        #region Relations
        public ICollection<UserRole>? UserRoles { get; set; }
        public ICollection<UserClaim>? UserClaims { get; set; }
        public  Employee? Employee { get; set; } // فروشنده مرتبط
        public  Shop? OwnedShop { get; set; }

        #endregion
    }
}
