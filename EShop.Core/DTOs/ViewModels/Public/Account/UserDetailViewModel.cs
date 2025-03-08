using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.DTOs.ViewModels.Public.Account
{
    public class UserDetailViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool HasShop { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
