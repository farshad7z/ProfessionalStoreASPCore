using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.DTOs.ViewModels.Admin.Product
{
    public class AddFeaturViewModel
    {
        public int ProductId { get; set; }
        public int FeatureId { get; set; }
        public string Value { get; set; }
    }
}
