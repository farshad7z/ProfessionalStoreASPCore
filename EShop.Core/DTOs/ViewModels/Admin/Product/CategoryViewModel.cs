namespace EShop.Core.DTOs.ViewModels.Admin.Product
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<CategoryViewModel> SubCategories { get; set; } = new();
    }

}
