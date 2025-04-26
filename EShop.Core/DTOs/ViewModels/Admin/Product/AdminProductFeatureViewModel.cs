using EShop.Core.Entities.Enums;

namespace EShop.Core.DTOs.ViewModels.Admin.Product
{
    public class AdminManageProductFeaturesViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        // لیست ویژگی‌های مرتبط با دسته‌های محصول
        public List<AdminFeatureItemViewModel> Features { get; set; }
        public List<AdminFeatureItemViewModel> SavedProductFeatures { get; set; }


    }

    public class AdminFeatureItemViewModel
    {
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }
        public FeatureType FeatureType { get; set; }

        // برای ویژگی‌های Text یا Number
        public string? Value { get; set; }

        // برای ویژگی‌های Range
        public decimal? RangeMin { get; set; }
        public decimal? RangeMax { get; set; }
    }
}
