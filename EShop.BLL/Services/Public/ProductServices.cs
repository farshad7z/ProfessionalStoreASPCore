using EShop.Core.Entities.Models;
using EShop.Core.Interfaces.Services.Public;
using EShop.Core.Interfaces.UnitOfWork;
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

        public async Task<int>? AddProductCategoryAsync(ProductSelectCategory model)
        {
            await _unitOfWork.Repository<ProductSelectCategory>().AddAsync(model);
            await _unitOfWork.SaveAsync();
            return model.Id;
        }
    }
}
