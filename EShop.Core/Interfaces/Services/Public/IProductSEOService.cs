using EShop.Core.DTOs.ViewModels.Admin.Product;
using EShop.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Interfaces.Services.Public
{
    public interface IProductSEOService
    {   /// <summary>
        /// دریافت اطلاعات سئو بر اساس Id محصول در سئو.
        /// </summary>
        Task<AdminProductSEOViewModel?> GetSEOByIdProductAsync(int productId);

        /// <summary>
        /// افزودن یا ویرایش سئو برای محصول .
        /// </summary>
        Task AddUpdateAsync(AdminProductSEOViewModel seo);
    }
}
