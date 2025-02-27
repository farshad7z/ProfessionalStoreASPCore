
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.Entities.Models
{
    public class User
    {

        [Key]        
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
        [MaxLength(300)]
        public required string PhoneNumber { get; set; }
        [MaxLength(300)]
        public string? ActiveCode { get; set; }
        public bool IsActive{ get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }

        #region Relations
        public ICollection<UserRole>? UserRoles { get; set; }
        #endregion
    }
}
