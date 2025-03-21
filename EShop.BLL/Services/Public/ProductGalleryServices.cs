using EShop.Core.Entities.Models;
using EShop.Core.Interfaces.Services.Public;
using EShop.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Services.Public
{


    public class ProductGalleryServices : IProductGalleryServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductGalleryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(ProductGallery gallery)
        {
             await _unitOfWork.Repository<ProductGallery>().AddAsync(gallery);
           await _unitOfWork.SaveAsync();
        }

        public async Task DeleleAsync(ProductGallery gallery)
        {
             await _unitOfWork.Repository<ProductGallery>().DeleteAsync(gallery);
            await _unitOfWork.SaveAsync();

        }

        public async Task<IEnumerable<ProductGallery>> GetGalleriesForProductAsync(int productId)
        {
            return await _unitOfWork.Repository<ProductGallery>().FindAsync(pg => pg.ProductId == productId);
        }
        public async Task<ProductGallery?> GetGalleryByIdAsync(int galleryId)
        {
            return await _unitOfWork.Repository<ProductGallery>().FindSingleOrDefaultAsync(pg => pg.GalleryId == galleryId);
        }
    }
}
