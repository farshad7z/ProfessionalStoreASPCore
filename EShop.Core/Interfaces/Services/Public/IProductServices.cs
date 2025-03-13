using EShop.Core.Entities.Models;
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
    }
}
