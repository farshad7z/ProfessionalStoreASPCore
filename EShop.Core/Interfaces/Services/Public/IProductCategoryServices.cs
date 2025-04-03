using EShop.Core.DTOs.ViewModels.Admin.Category;
using EShop.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using EShop.Core.Entities.Models.Products;

namespace EShop.Core.Interfaces.Services.Public
{
    public interface IProductCategoryServices
    {
        /// <summary>
        /// به طور غیر همزمان تمامی دسته‌بندی‌های موجود را برمی‌گرداند.
        /// </summary>
        /// <returns>یک مجموعه از دسته‌بندی‌ها (ProductCategory)</returns>
        Task<IEnumerable<ProductCategory>> GetAllAsync();

        /// <summary>
        /// به طور غیر همزمان تمامی دسته‌بندی‌هایی که برای منو مناسب هستند را برمی‌گرداند.
        /// </summary>
        /// <returns>یک مجموعه از دسته‌بندی‌ها که برای منو مناسب هستند (ProductCategory)</returns>
        Task<IEnumerable<ProductCategory>> GetCategoryForMenuAsync();

        /// <summary>
        /// به طور غیر همزمان تمامی دسته‌بندی‌هایی که باید در صفحه اصلی نمایش داده شوند را برمی‌گرداند.
        /// </summary>
        /// <returns>یک مجموعه از دسته‌بندی‌ها که باید در صفحه اصلی نمایش داده شوند (ProductCategory)</returns>
        Task<IEnumerable<ProductCategory>> GetCategoryOnMainPageAsync();

        /// <summary>
        /// به طور غیر همزمان تمامی دسته‌بندی‌ها را برای صفحه اصلی مدیریت دسته‌بندی‌ها برمی‌گرداند.
        /// </summary>
        /// <returns>یک مجموعه از دسته‌بندی‌ها برای صفحه اصلی مدیریت (AdminProductCategoriesOnIndexViewModel)</returns>
        Task<IEnumerable<AdminProductCategoriesOnIndexViewModel>> GetAllForIndexCategoryAsync();

        /// <summary>
        /// به طور غیر همزمان تمامی دسته‌بندی‌ها را برای انتخاب والد برای دسته‌بندی‌های جدید برمی‌گرداند.
        /// </summary>
        Task<List<SelectListItem>> GetAllForSelectParentAsync();

        /// <summary>
        /// به طور غیر همزمان با اساس نام ورودی  دسته‌بندی را برمی‌گرداند.
        /// </summary>
        /// <param name="name"> (نام) موجودیت مورد نظر.</param>
        Task<ProductCategory> GetCategoryByNameAsync(string name);

        /// <summary>
        /// بررسی می‌کند که آیا نام دسته‌بندی قبلاً ثبت شده است یا نه.
        /// </summary>
        /// <param name="name">نام دسته‌بندی</param>
        /// <returns>مقدار true اگر وجود دارد، در غیر این‌صورت false</returns>
        Task<bool> IsCategoryNameExistsAsync(string name);

        /// <summary>
        /// بررسی می‌کند که آیا Slug دسته‌بندی تکراری است یا نه.
        /// </summary>
        /// <param name="slug">Slug دسته‌بندی</param>
        /// <returns>مقدار true اگر وجود دارد، در غیر این‌صورت false</returns>
        Task<bool> IsCategorySlugExistsAsync(string slug);

    
        /// <summary>
        ///افزودن دسته بندی جدید.
        /// </summary>
        Task<int> AddAsync(ProductCategory model);

        /// <summary>
        /// به طور غیرهمزمان اطلاعات دسته‌بندی را بر اساس شناسه برمی‌گرداند.
        /// </summary>
        /// <param name="id">شناسه دسته‌بندی</param>
        /// <returns>دسته‌بندی مربوطه</returns>
        Task<ProductCategory> GetCategoryByIdAsync(int id);

        /// <summary>
        /// به طور غیرهمزمان ویژگی‌های مرتبط با یک دسته‌بندی خاص را بر اساس شناسه آن برمی‌گرداند.
        /// </summary>
        /// <param name="id">شناسه دسته‌بندی</param>
        /// <returns>لیستی از ویژگی‌های دسته‌بندی</returns>
        Task<IEnumerable<AdminFeatureViewModel>> GetFeaturesByCategoryIdAsync(int categoryId);

        /// <summary>
        /// بررسی می‌کند که آیا ویژگی در دسته‌بندی قبلاً ثبت شده است یا نه.
        /// </summary>
        /// <param name="categoryId">شناسه دسته‌بندی</param>
        /// <param name="slectFeatureIds">لیت شناسه ویژگی های انتخاب شده</param>
        /// <returns>مقدار true اگر وجود دارد، در غیر این‌صورت false</returns>
        Task<bool> IsCategoryFeatureExistsAsync(int categoryId , List<int> slectFeatureIds);
        /// <summary>
        ///افزودن ویژگی ها به دسته بندی .
        /// </summary>
        Task<int> AddFeatureInProductCategoryAsync(List<CategoryFeatureValue> model);
        /// <summary>
        /// پاک کردن  ویژگی از دسته بندی محصول .
        /// </summary>
        Task RemoveFeatureFromCategoryAsync(CategoryFeatureValue categoryFeatureValue);

        /// <summary>
        /// به طور غیرهمزمان اطلاعات ویژگی مرتبط با دسته‌بندی را بر اساس شناسه‌های ورودی برمی‌گرداند.
        /// </summary>
        /// <param name="categoryId">شناسه دسته‌بندی</param>
        /// <param name="featureId">شناسه ویژگی</param>
        /// <returns>مقدار ویژگی دسته‌بندی، یا `null` اگر پیدا نشد</returns>
        Task<CategoryFeatureValue?> GetCategoryFeatureByCategoryIdAndFeatureIdAsync(int categoryId, int featureId);

    }
}
