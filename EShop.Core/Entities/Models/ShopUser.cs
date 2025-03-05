using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models
{
    public class ShopUser
    {
        public int ShopUserId { get; set; }

        // رابطه با فروشگاه
        public int ShopId { get; set; }

        // رابطه با کاربر
        public required string UserId { get; set; }

        public bool IsActivOnShop { get; set; }

        // تاریخ عضویت کاربر در فروشگاه
        public DateTime JoinedAt { get; set; }

        #region Relations
        public required User User { get; set; }
        public required Shop Shop { get; set; }
        #endregion

    }
}

