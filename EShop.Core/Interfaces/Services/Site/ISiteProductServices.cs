using EShop.Core.DTOs.ViewModels.Product;
using EShop.Core.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.Core.Interfaces.Services.Site
{
    public interface ISiteProductServices
    {
        /// <summary>
        /// دریافت لیستی از محصولات با فیلترهای مختلف.
        /// این متد بر اساس نوع فیلتر تعیین‌شده، لیستی از محصولات را برمی‌گرداند.
        /// </summary>
        /// <param name="filterType">نوع فیلتر برای انتخاب محصولات (آخرین محصولات، پرفروش‌ترین‌ها، پر بازدیدترین‌ها، جستجو).</param>
        /// <param name="searchTerm">عبارت جستجو برای فیلتر کردن محصولات (در صورت انتخاب فیلتر جستجو).</param>
        /// <returns>لیستی از محصولات فیلترشده به‌صورت `IEnumerable<ProductViewModel>`.</returns>
        Task<IEnumerable<ProductViewModel>> GetFilteredProductsAsync(ProductFilterType filterType, string searchTerm = "");

        /// <summary>
        /// دریافت اطلاعات یک محصول بر اساس شناسه آن.
        /// </summary>
        /// <param name="productId">شناسه محصول مورد نظر.</param>
        /// <returns>یک `ProductViewModel` مربوط به محصول مورد نظر یا `null` در صورت عدم وجود.</returns>
        Task<ProductDetailViewModel> GetProductByIdAsync(int productId);
    }
}
