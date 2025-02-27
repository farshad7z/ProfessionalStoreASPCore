using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models
{
    public class UserRole
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        #region Relations
        public  User? User { get; set; }
        public  Role? Role { get; set; }
        #endregion
    }
}
