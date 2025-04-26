using EShop.Core.Entities.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Interfaces.Services.Public
{
    public interface IVariantServices
    {
        /// <summary>
        /// دریافت واریانت‌های محصول بر اساس شناسه محصول.
        /// </summary>
        /// <param name="productId">شناسه محصول</param>
        /// <returns>لیست واریانت‌های مربوط به محصول</returns>
        Task<IEnumerable<ProductVariant>> GetVariantsByProductIdAsync(int productId);

    }
}
