using EShop.Core.Entities.Models;
using EShop.Core.Interfaces.Services.Public;
using EShop.Core.Interfaces.UnitOfWork;
using EShop.DAL.UnitOfWork;
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
        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
           return await _unitOfWork.Repository<ProductCategory>().GetAllAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetCategoryForMenu()
        {
            return await _unitOfWork.Repository<ProductCategory>().FindAsync(pc=>pc.MenuType != Core.Enums.MenuType.CategoryOnly);
        }

        public async Task<IEnumerable<ProductCategory>> GetCategoryOnMainPage()
        {
            return await _unitOfWork.Repository<ProductCategory>().FindAsync(pc => pc.MenuType != Core.Enums.MenuType.CategoryOnly && pc.IsCategoryOnMain==true);
        }
    }
}
