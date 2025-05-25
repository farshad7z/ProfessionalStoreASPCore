using EShop.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.DTOs.ViewModels.Admin.Product
{
    public class AdminFeatureItemViewModel
    {
        public int FeatureId { get; set; }
        public int? FeatureValueId { get; set; }
        public string? FeatureName { get; set; }
        public FeatureType FeatureType { get; set; }
        public string? Value { get; set; }
        public decimal? RangeMin { get; set; }
        public decimal? RangeMax { get; set; }
    }
}
