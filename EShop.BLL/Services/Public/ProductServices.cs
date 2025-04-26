using EShop.Core.Entities.Models;
using EShop.Core.Interfaces.Services.Public;
using EShop.Core.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int>? AddAsync(Product model)
        {
            await  _unitOfWork.Repository<Product>().AddAsync(model);
            await _unitOfWork.SaveAsync();
            return model.Id;
        }

        public Task<bool> AddFeatureToProductAsync(int productId, int featureId, string value)
        {
            throw new NotImplementedException();
        }

        public async Task<int>? AddProductCategoryAsync(ProductSelectCategory model)
        {
            await _unitOfWork.Repository<ProductSelectCategory>().AddAsync(model);
            await _unitOfWork.SaveAsync();
            return model.Id;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Product>().GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsWithCategoriesAsync()
        {
            return await _unitOfWork.Repository<Product>()
        .GetAllAsync(includeProperties: "ProductSelectCategory.ProductCategory");
        }

        public async Task<Product?> GetByIdAndIncludeSelectProductCategoryAsync(int productId)
        {
            return await _unitOfWork.Repository<Product>().GetFirstOrDefaultAsync(
                predicate: p => p.Id == productId,
                include: q => q.Include(p => p.ProductSelectCategory)
            );
        }


        public async Task<Product?> GetByIdAsync(int productId)
        {
            return await _unitOfWork.Repository<Product>().GetByIdAsync(productId);
        }

        public async Task<IEnumerable<int>> GetProductSelectCategoriesByIdAsync(int productId)
        {
            var productCategories = await _unitOfWork.Repository<ProductSelectCategory>()
                .FindAsync(pc => pc.ProductId == productId); 

            return productCategories.Select(pc => pc.ProductCategoryId);
        }



        public async Task UpdateAsync(Product product)
        {
            _unitOfWork.Repository<Product>().Update(product);
            await _unitOfWork.SaveAsync(); 
        }

        public async Task UpdateProductSelectCategoriesAsync(int productId, List<int> newCategoryIds)
        {
            var productSelectCategory = await _unitOfWork.Repository<ProductSelectCategory>().FindAsync(pc=>pc.ProductId==productId);
            // حذف دسته‌بندی‌های فعلی محصول

            await _unitOfWork.Repository<ProductSelectCategory>().DeleteRangeAsync(productSelectCategory);

            // افزودن دسته‌بندی‌های جدید
            var newCategories = newCategoryIds.Select(categoryId => new ProductSelectCategory
            {
                ProductId = productId,
                ProductCategoryId = categoryId
            }).ToList();

            await _unitOfWork.Repository<ProductSelectCategory>().AddRangeAsync(newCategories);

            await _unitOfWork.SaveAsync();
        }

    }
}
