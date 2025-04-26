namespace EShop.Core.DTOs.ViewModels.Admin.Product
{
    public class AdminManageProductVariantsViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        // لیست ویژگی‌های محصول (برای انتخاب مقادیر واریانت)
        public List<AdminFeatureValueItemViewModel> FeatureValues { get; set; }

        // لیست واریانت‌های فعلی
        public List<AdminProductVariantViewModel> ExistingVariants { get; set; }

        // مدل برای اضافه کردن واریانت جدید
        public AdminAddProductVariantViewModel NewVariant { get; set; }
    }

    public class AdminFeatureValueItemViewModel
    {
        // این مدل می‌تواند ترکیبی از Feature و مقدار ذخیره‌شده باشد.
        public int ProductFeatureValueId { get; set; }
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }
        public string Value { get; set; }
    }

    public class AdminProductVariantViewModel
    {
        public int Id { get; set; }
        public string VariantName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        // لیستی از مقادیر ویژگی‌های انتخاب‌شده برای این واریانت
        public List<string> VariantFeatureValues { get; set; }
    }

    public class AdminAddProductVariantViewModel
    {
        public int ProductId { get; set; }
        public string VariantName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        // شناسه‌های مقادیر ویژگی انتخاب‌شده برای این واریانت
        public List<int> SelectedProductFeatureValueIds { get; set; }
    }
}
