using EShop.Core.Entities.Models.Products;
using EShop.Core.Interfaces.Services.Public;
using EShop.Core.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Services.Public
{
  public class VariantServices: IVariantServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public VariantServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductVariant>> GetVariantsByProductIdAsync(int productId)
        {
            // استفاده از Repository برای دریافت واریانت‌های محصول
            var result = await _unitOfWork.Repository<ProductVariant>()
                .GetAllWithIncludeAsync(
                    c => c.ProductId == productId, // فیلتر بر اساس شناسه محصول
                    include: query => query.Include(c => c.VariantFeatures) // شامل ویژگی‌های واریانت
                );

            return result;
        }
    }
}
