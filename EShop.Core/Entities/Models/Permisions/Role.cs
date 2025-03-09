using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models
{
    public class Role
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)] // مقدار ID به صورت دستی تنظیم می‌شود و توسط دیتابیس تولید نمی‌شود
        public int RoleId { get; set; }
        [Display(Name = "عنوان سیستمی نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از{1} کاراکتر باشد.")]
        public required string RoleName{ get; set; }
       
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از{1} کاراکتر باشد.")]
        public required string RoleTitle { get; set; }

        #region Relations
        public ICollection<UserRole>? UserRoles { get; set; }
        #endregion

    }
}
