using EShop.Core.DTOs.ViewModels.Product;

namespace EShop.Core.DTOs.ViewModels.Customer
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<ProductViewModel>? Products { get; set; }
    }
}
