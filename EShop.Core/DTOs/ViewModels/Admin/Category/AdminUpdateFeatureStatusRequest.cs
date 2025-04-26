using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.DTOs.ViewModels.Admin.Category
{
    public class AdminUpdateFeatureStatusRequest
    {
        public int CategoryId { get; set; }
        public int FeatureId { get; set; }
        public bool IsRequired { get; set; }
        public bool IsVariant { get; set; }
    }

}
