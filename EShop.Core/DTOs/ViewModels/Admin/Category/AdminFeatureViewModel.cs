namespace EShop.Core.DTOs.ViewModels.Admin.Category
{
    public class AdminFeatureViewModel
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsVariant { get; set; }
        public bool IsRequired { get; set; }


    }
}