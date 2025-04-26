using EShop.Core.DTOs.ViewModels.Admin.Category;
using EShop.Core.Entities.Models;
using EShop.Core.Entities.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Interfaces.Services.Public
{
    public interface IFeatureService
    {
        /// <summary>
        /// ویژگی جدیدی را به صورت غیرهمزمان به سیستم اضافه می‌کند.
        /// </summary>
        /// <param name="model">مدل ویژگی که باید اضافه شود.</param>
        /// <returns>وظیفه‌ای که نمایانگر عملیات غیرهمزمان است و نتیجه آن شناسه ویژگی اضافه شده است.</returns>
        Task<int> AddFeatureAsync(Feature model);

        /// <summary>
        /// تمام ویژگی‌ها را از سیستم به صورت غیرهمزمان دریافت می‌کند.
        /// </summary>
        /// <param name="model">مدل حاوی هرگونه فیلتر یا پارامتر جستجو. این پارامتر برای فیلتر کردن نتایج استفاده می‌شود، هرچند در این متد الزامی نیست.</param>
        /// <returns>وظیفه‌ای که نمایانگر عملیات غیرهمزمان است و نتیجه آن مجموعه‌ای از ویژگی‌ها می‌باشد.</returns>
        Task<IEnumerable<Feature>> GetAllFeaturesAsync();

        /// <summary>
        /// ویژگی خاصی را بر اساس شناسه منحصر به فرد آن به صورت غیرهمزمان دریافت می‌کند.
        /// </summary>
        /// <param name="model">مدل ویژگی که حاوی شناسه ویژگی مورد نظر برای دریافت است.</param>
        /// <returns>وظیفه‌ای که نمایانگر عملیات غیرهمزمان است و نتیجه آن ویژگی با شناسه داده شده است.</returns>
        Task<Feature> GetFeatureByIdAsync(int id);
        /// <summary>
        /// اپدیت اطلاعات ویژگی.
        /// </summary>
        Task UpdateFeatureAsync(Feature model);

        /// <summary>
        /// دریافت لیست ویژگی‌های مرتبط با دسته‌بندی‌های مشخص.
        /// فقط ویژگی‌هایی که به دسته‌بندی‌ها تخصیص داده شده‌اند، بازگردانده می‌شوند.
        /// </summary>
        /// <param name="categoryIds">لیستی از شناسه‌های دسته‌بندی.</param>
        /// <returns>لیستی از ویژگی‌ها (با مقداردهی) که مربوط به دسته‌بندی‌های ورودی هستند.</returns>
        Task<IEnumerable<CategoryFeature>> GetListFeaturesByCategoryIdsAsync(List<int> categoryIds);

        /// <summary>
        /// دریافت لیست ویژگی‌های مرتبط با محصول مشخص.
        /// فقط ویژگی‌هایی که به محصول تخصیص داده شده‌اند، بازگردانده می‌شوند.
        /// </summary>
        /// <param name="productId">شناسه محصول.</param>
        /// <returns>لیستی از ویژگی‌ها (با مقداردهی) که مربوط به محصول ورودی هستند.</returns>
        Task<IEnumerable<ProductFeature>> GetListFeaturesValuesByProductIdAsync(int categoryId);

    }
}
