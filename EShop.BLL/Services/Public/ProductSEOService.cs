using EShop.Core.DTOs.ViewModels.Admin.Product;
using EShop.Core.Entities.Models;
using EShop.Core.Interfaces.Services.Public;
using EShop.Core.Interfaces.UnitOfWork;

namespace EShop.BLL.Services.Public
{
    public class ProductSEOService : IProductSEOService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductSEOService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddUpdateAsync(AdminProductSEOViewModel model)
        {
            var existingSEO = await _unitOfWork.Repository<ProductSEO>().FindSingleOrDefaultAsync(seo => seo.ProductId == model.ProductId);

            if (existingSEO != null)
            {
                // به‌روزرسانی اطلاعات سئو
                existingSEO.MetaTitle = model.MetaTitle;
                existingSEO.MetaDescription = model.MetaDescription;
                existingSEO.MetaKeywords = model.MetaKeywords;
                existingSEO.Slug = model.Slug ?? "-";

                _unitOfWork.Repository<ProductSEO>().Update(existingSEO);
            }
            else
            {
                // ایجاد اطلاعات سئو جدید
                var newSEO = new ProductSEO
                {
                    ProductId = model.ProductId,
                    MetaTitle = model.MetaTitle,
                    MetaDescription = model.MetaDescription,
                    MetaKeywords = model.MetaKeywords,
                    Slug = model.Slug ?? "-"
                };

                await _unitOfWork.Repository<ProductSEO>().AddAsync(newSEO);
            }

            await _unitOfWork.SaveAsync();
        }


        public async Task<AdminProductSEOViewModel?> GetSEOByIdProductAsync(int productId)
        {
            var seo = await _unitOfWork.Repository<ProductSEO>().FindSingleOrDefaultAsync(seo => seo.ProductId == productId);
            if (seo != null)
            {
                return new AdminProductSEOViewModel()
                {
                    ProductId = seo.ProductId,
                    MetaDescription = seo.MetaDescription,
                    MetaKeywords = seo.MetaKeywords,
                    MetaTitle = seo.MetaTitle,
                    Slug = seo.Slug ?? "-"
                };
            }
            else
                return new AdminProductSEOViewModel() { ProductId= productId };
        }

    }
}
