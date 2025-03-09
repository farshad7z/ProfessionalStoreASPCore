using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Models
{
   public class AppClaim
    {
        public AppClaim()
        {

        }

        public int ClaimId { get; set; }
        public required string ClaimType { get; set; }
        [MaxLength(300)]
        public required string Value { get; set; }

        #region Relation
        public ICollection<UserClaim>? UserClaims { get; set; }
        #endregion
    }
}
