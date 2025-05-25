using EShop.Core.Entities.Models;
using EShop.Core.Entities.Models.Products;
using EShop.Core.Interfaces.Services.Public;
using EShop.Core.Interfaces.UnitOfWork;
using EShop.Core.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
            await _unitOfWork.Repository<Product>().AddAsync(model);
            await _unitOfWork.SaveAsync();
            return model.Id;
        }

        public async Task<ResponseModel<bool>> AddFeatureToProductAsync(int productId, int featureId, string value)
        {
            // بررسی اینکه آیا این ویژگی با این مقدار قبلاً برای این محصول ثبت شده است یا خیر
            var isExistFeature = await _unitOfWork.Repository<ProductFeature>().ExistsAsync(f =>
                f.ProductId == productId &&
                f.FeatureId == featureId &&
                f.Value == value);

            if (isExistFeature)
            {
                return ResponseModel<bool>.Fail("این ویژگی با این مقدار قبلاً اضافه شده است", 403);
            }
            var feature = await _unitOfWork.Repository<Feature>().ExistsAsync(f => f.Id == featureId);

            
            if (!feature)
            {
                return ResponseModel<bool>.Fail("ویژگی انتخاب شده نامعتبر است", 404);
            }
            try
            {
                // ساخت شیء ویژگی محصول جدید
                var model = new ProductFeature
                {
                    ProductId = productId,
                    FeatureId = featureId,
                    Value = value
                };

                // افزودن ویژگی به دیتابیس
                await _unitOfWork.Repository<ProductFeature>().AddAsync(model);
                await _unitOfWork.SaveAsync();

                // بررسی موفقیت عملیات
                if (model.Id > 0)
                {
                    return ResponseModel<bool>.Success(true, "ویژگی مدنظر با موفقیت به محصول اضافه شد");
                }
                else
                {
                    return ResponseModel<bool>.Fail("خطایی در افزودن ویژگی مدنظر رخ داده است", 500);
                }
            }
            catch (Exception ex)
            {
                // TODO: در اینجا می‌توان از ILogger برای ثبت خطا استفاده کرد
                // _logger.LogError(ex, "خطا در افزودن ویژگی به محصول");

                return ResponseModel<bool>.Fail("یک خطای پیش‌بینی‌نشده هنگام افزودن ویژگی رخ داد", 500);
            }
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
            var productSelectCategory = await _unitOfWork.Repository<ProductSelectCategory>().FindAsync(pc => pc.ProductId == productId);
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
