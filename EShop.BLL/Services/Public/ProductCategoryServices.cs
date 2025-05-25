using EShop.Core.DTOs.ViewModels.Admin.Category;
using EShop.Core.Entities.Models;
using EShop.Core.Interfaces.Services.Public;
using EShop.Core.Interfaces.UnitOfWork;
using EShop.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using EShop.Core.Entities.Models.Products;
namespace EShop.BLL.Services.Public
{
    public class ProductCategoryServices : IProductCategoryServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(ProductCategory model)
        {
            await _unitOfWork.Repository<ProductCategory>().AddAsync(model);
            await _unitOfWork.SaveAsync();
            return model.CategoryId;
        }

        public async Task<int> AddFeatureInProductCategoryAsync(List<CategoryFeature> model)
        {
            // اضافه کردن ویژگی‌ها به دسته‌بندی
            await _unitOfWork.Repository<CategoryFeature>().AddRangeAsync(model);

            // ذخیره تغییرات
            await _unitOfWork.SaveAsync();

            // بازگشت تعداد رکوردهای افزوده شده
            return model.Count;
        }


        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _unitOfWork.Repository<ProductCategory>().GetAllAsync();
        }

      public async Task<IEnumerable<AdminProductCategoriesOnIndexViewModel>> GetAllForIndexCategoryAsync()
{
    var categories = await _unitOfWork.Repository<ProductCategory>()
        .GetAllWithIncludeAsync(include: query => query.Include(c => c.Parent).Include(c => c.Children));

    var categoryList = await Task.WhenAll(categories.Select(async c => new AdminProductCategoriesOnIndexViewModel
    {
        CategoryId = c.CategoryId,
        Name = c.Name,
        Description = c.Description,
        ParentId = c.Parent != null ? new Dictionary<int, string> { { c.Parent.CategoryId, c.Parent.Name } } : null,
        child = c.Children.Select(child => new ProductCategoriesChildViewModel
        {
            Id = child.CategoryId,
            Name = child.Name
        }).ToList(),
        MenuType = c.MenuType,
        //Image = c.IconClass,
        Image = c.ImageName,
        Slug = c.Slug,
        CreatedAt = c.CreatedAt,
        UpdatedAt = c.UpdatedAt,
        IsCategoryOnMain = c.IsCategoryOnMain,

        // ✅ فقط اگر خودش از نوع TertiaryMenu بود و والد بود
        IsParent = c.MenuType == Core.Enums.MenuType.TertiaryMenu &&
                   categories.Any(pc => pc.ParentId == c.CategoryId),

        IsDeleted = c.IsDeleted
    }));

    return categoryList;
}


        public async Task<List<SelectListItem>> GetAllForSelectParentAsync()
        {
            // دریافت دسته‌بندی‌ها با شرایط خاص
            var categories = await _unitOfWork.Repository<ProductCategory>()
                .GetAllAsync(pc => pc.IsDeleted == false
                                   && pc.MenuType != Core.Enums.MenuType.CategoryOnly
                                   && pc.MenuType != Core.Enums.MenuType.TertiaryMenu);

            // تبدیل داده‌ها به SelectListItem
            var categoryItems = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            }).ToList();

            return categoryItems;
        }

        public async Task<ProductCategory> GetCategoryByIdAsync(int id)
        {
            return await _unitOfWork.Repository<ProductCategory>()
                 .FindSingleOrDefaultAsync(pc => pc.CategoryId == id);
        }

        public async Task<ProductCategory> GetCategoryByNameAsync(string name)
        {
            return await _unitOfWork.Repository<ProductCategory>().FindSingleOrDefaultAsync(pc => pc.Name == name);

        }

        public async Task<CategoryFeature?> GetCategoryFeatureByCategoryIdAndFeatureIdAsync(int categoryId, int featureId)
        {
            return await _unitOfWork.Repository<CategoryFeature>()
                .FindSingleOrDefaultAsync(cfv => cfv.CategoryId == categoryId && cfv.FeatureId == featureId);
        }

        public async Task<IEnumerable<ProductCategory>> GetCategoryForMenuAsync()
        {
            return await _unitOfWork.Repository<ProductCategory>().FindAsync(pc => pc.MenuType != Core.Enums.MenuType.CategoryOnly);
        }

        public async Task<IEnumerable<ProductCategory>> GetCategoryOnMainPageAsync()
        {
            return await _unitOfWork.Repository<ProductCategory>().FindAsync(pc => pc.MenuType != Core.Enums.MenuType.CategoryOnly && pc.IsCategoryOnMain == true);
        }

        public async Task<IEnumerable<AdminFeatureViewModel>> GetFeaturesByCategoryIdAsync(int categoryId)
        {
            var categoryFeatures = await _unitOfWork.Repository<CategoryFeature>()
                .GetAllWithIncludeAsync(
                    cfv => cfv.CategoryId == categoryId,
                    query => query.Include(cfv => cfv.Feature) // Eager Loading برای Feature
                );

            return categoryFeatures.Select(scf => new AdminFeatureViewModel
            {
                Id = scf.Id,
                FeatureId = scf.FeatureId,
                Name = scf.Feature.Name,
                CategoryId=categoryId,
                IsRequired=scf.IsRequired,
                IsVariant=scf.IsVariant
            });
        }

        public async Task<bool> IsCategoryFeatureExistsAsync(int categoryId, List<int> selectFeatureIds)
        {
            // پیدا کردن ویژگی‌هایی که به این دسته‌بندی و ویژگی‌ها تعلق دارند
            var existingFeatures = await _unitOfWork.Repository<CategoryFeature>()
                .FindAsync(cfv => cfv.CategoryId == categoryId && selectFeatureIds.Contains(cfv.FeatureId));

            // اگر ویژگی‌هایی یافت شوند، به این معناست که قبلاً اضافه شده‌اند
            return existingFeatures.Any();
        }

        public async Task<bool> IsCategoryNameExistsAsync(string name,int? parentd)
        {
            return await _unitOfWork.Repository<ProductCategory>().ExistsAsync(pc => pc.Name == name && pc.ParentId== parentd);
        }

        public async Task<bool> IsCategorySlugExistsAsync(string slug)
        {
            return await _unitOfWork.Repository<ProductCategory>().ExistsAsync(pc => pc.Slug == slug);
        }

        public async Task RemoveFeatureFromCategoryAsync(CategoryFeature categoryFeatureValue)
        {

            await _unitOfWork.Repository<CategoryFeature>().DeleteAsync(categoryFeatureValue);
            await _unitOfWork.SaveAsync();
        }

        public async Task<int> UpdateCategoryFeatureAsync(CategoryFeature model)
        {
            // Find the existing category feature by its Id
            var existingFeature = await _unitOfWork.Repository<CategoryFeature>()
                .ExistsAsync(cfv => cfv.Id == model.Id);

            if (existingFeature)
            {
                 _unitOfWork.Repository<CategoryFeature>().Update(model);

                // Update the necessary properties of the category feature


                // Save the changes to the database
                await _unitOfWork.SaveAsync();

                return 1; // Successfully updated
            }

            return 0; // If the feature doesn't exist, return 0 (no update)
        }

    }
}
