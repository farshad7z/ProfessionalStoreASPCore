using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.DTOs.ViewModels.Admin.Category
{
    public class AdminAddFeatureRequest
    {
        public int CategoryId { get; set; }
        public List<int> FeatureIds { get; set; }
    }


    public class AdminDeleteFeatureRequest
    {
        public int CategoryId { get; set; }
        public int FeatureId { get; set; }
    }
}
