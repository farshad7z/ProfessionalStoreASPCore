using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models
{
    public class UserClaim
    {
        public int ClaimId { get; set; }
        public int UserId { get; set; }
        public required string ClaimValue { get; set; }

        #region Relations
        public AppClaim? Claim { get; set; }
        public User? User { get; set; }

        #endregion
    }
}
