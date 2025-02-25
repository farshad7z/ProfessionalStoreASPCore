using EShop.Core.DTOs.ViewModels.Product;

namespace EShop.Core.DTOs.ViewModels.Customer
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
