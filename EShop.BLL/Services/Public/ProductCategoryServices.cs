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

namespace EShop.BLL.Services.Public
{
   public class ProductCategoryServices : IProductCategoryServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
           return await _unitOfWork.Repository<ProductCategory>().GetAllAsync();
        }

        public async Task<IEnumerable<AdminProductCategoriesOnIndexViewModel>> GetAllForIndexCategoryAsync()
        {
            var categories = await _unitOfWork.Repository<ProductCategory>()
                .GetAllWithIncludeAsync(include: query => query.Include(c => c.Parent).Include(c => c.Children));

            return categories.Select(c => new AdminProductCategoriesOnIndexViewModel
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
                Image = c.IconClass,
                Slug = c.Slug,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsCategoryOnMain = c.IsCategoryOnMain,
                IsDeleted = c.IsDeleted
            }).ToList();
        }


        public async Task<IEnumerable<ProductCategory>> GetCategoryForMenuAsync()
        {
            return await _unitOfWork.Repository<ProductCategory>().FindAsync(pc=>pc.MenuType != Core.Enums.MenuType.CategoryOnly);
        }

        public async Task<IEnumerable<ProductCategory>> GetCategoryOnMainPageAsync()
        {
            return await _unitOfWork.Repository<ProductCategory>().FindAsync(pc => pc.MenuType != Core.Enums.MenuType.CategoryOnly && pc.IsCategoryOnMain==true);
        }
    }
}
