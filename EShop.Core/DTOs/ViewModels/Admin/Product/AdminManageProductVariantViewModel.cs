using EShop.Core.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace EShop.Core.DTOs.ViewModels.Admin.Product
{


    public class AdminManageProductVariantsViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<AdminFeatureItemViewModel> FeatureValues { get; set; } = new List<AdminFeatureItemViewModel>();
        public List<AdminVariantItemViewModel> SavedVariants { get; set; } = new List<AdminVariantItemViewModel>();
        public List<AdminAddVariantViewModel> AddVariants { get; set; } = new List<AdminAddVariantViewModel>();
    }


    public class AdminVariantItemViewModel
    {
        public int VariantId { get; set; }
        public string VariantName { get; set; }
        public decimal? Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public List<AdminFeatureItemViewModel> VariantFeatures { get; set; } = new List<AdminFeatureItemViewModel>();
    }

    public class AdminAddVariantViewModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "نام واریانت الزامی است")]
        [MaxLength(250)]
        public string VariantName { get; set; }
        public decimal? Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "موجودی نمی‌تواند منفی باشد")]
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; } = true;
        public List<int> SelectedFeatureValueIds { get; set; } = new List<int>(); // شناسه مقادیر ویژگی‌ها
    }
}