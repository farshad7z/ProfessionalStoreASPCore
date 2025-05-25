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

}
