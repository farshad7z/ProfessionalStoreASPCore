using EShop.Core.Entities.Models;
using EShop.Core.Entities.Models.Products;
using EShop.Core.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Interfaces.Services.Public
{
    public interface IProductServices
    {
        /// <summary>
        ///افزودن محصول جدید.
        /// </summary>
        Task<int> AddAsync(Product model);

        /// <summary>
        ///افزودن دسته بندی به محصولات .
        /// </summary>
        Task<int>? AddProductCategoryAsync(ProductSelectCategory model);

        /// <summary>
        ///دریاقت لیست همه محصولات.
        /// </summary>
        Task<IEnumerable<Product>> GetAllAsync();
        /// <summary>
        ///دریاقت لیست همه محصولات به همراه دسته بندی های آن.
        /// </summary>
        Task<IEnumerable<Product>> GetAllProductsWithCategoriesAsync();
        /// <summary>
        /// براساس Id دریاقت  اطلاعات محصولات.
        /// </summary>
        Task<Product> GetByIdAsync(int productId);
        /// <summary>
        /// اپدیت اطلاعات محصولات.
        /// </summary>
        Task UpdateAsync(Product product);
        /// <summary>
        /// حذف دسته بندی های قبلی مربوط به محصول و افرودن دسته بندی جدید به محصول
        /// </summary>
        Task UpdateProductSelectCategoriesAsync(int productId, List<int> newCategoryIds);
        /// <summary>
        /// دریافت اطلاعات محصول به همراه دسته‌بندی‌های انتخاب‌شده.
        /// </summary>
        Task<Product> GetByIdAndIncludeSelectProductCategoryAsync(int productId);
        /// <summary>
        /// دریافت لیست دسته بندی های یک محصول با Id
        /// /// </summary>
        Task<IEnumerable<int>> GetProductSelectCategoriesByIdAsync(int productId);
        /// <summary>
        ///افزودن ویژگی جدید به محصول .
        /// </summary>
        Task<ResponseModel<bool>> AddFeatureToProductAsync(int productId, int featureId, string value);
    }
}
