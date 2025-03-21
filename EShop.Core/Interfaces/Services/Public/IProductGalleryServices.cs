
using EShop.Core.Entities.Models;

namespace EShop.Core.Interfaces.Services.Public
{
    public interface IProductGalleryServices
    {
        /// <summary>
        /// دریافت تمام عکس های گالری مربوط به محصول .
        /// </summary>
        Task<IEnumerable<ProductGallery>> GetGalleriesForProductAsync(int ProductId);
        /// <summary>
        /// دریافت  عکس از گالری  با Id .
        /// </summary>
        Task<ProductGallery?> GetGalleryByIdAsync(int galleryId);
        /// <summary>
        /// افزودن  عکس به گالری محصول .
        /// </summary>
        Task AddAsync(ProductGallery gallery);
        /// <summary>
        /// پاک کردن  عکس از گالری محصول .
        /// </summary>
        Task DeleleAsync(ProductGallery gallery);
    }
}
