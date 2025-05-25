using EShop.Core.Entities.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Core.Interfaces.Services.Public
{
    public interface IVariantServices
    {
        /// <summary>
        /// دریافت لیست واریانت‌های مرتبط با محصول خاص.
        /// </summary>
        /// <param name="productId">شناسه محصول</param>
        /// <returns>لیست واریانت‌های محصول</returns>
        Task<IEnumerable<ProductVariant>> GetVariantsByProductIdAsync(int productId);

        /// <summary>
        /// افزودن یک واریانت جدید به محصول.
        /// </summary>
        /// <param name="model">مدل واریانت</param>
        /// <returns>شناسه واریانت ایجاد شده</returns>
        Task<int> AddVariantToProductAsync(ProductVariant model);

        /// <summary>
        /// افزودن مجموعه‌ای از مقادیر ویژگی به یک واریانت محصول.
        /// </summary>
        /// <param name="productVariantValueId">شناسه واریانت محصول</param>
        /// <param name="featureValueIds">لیست شناسه مقادیر ویژگی</param>
        /// <returns>تعداد آیتم‌های ثبت شده</returns>
        Task<int> AddRangeFeatureToProductVariantFeatureAsync(int productVariantValueId, List<int> featureValueIds);
    }
}
