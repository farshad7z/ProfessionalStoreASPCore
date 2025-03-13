using EShop.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Interfaces.Services.Public
{
    public interface IProductCategoryServices
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync();
       Task<IEnumerable<ProductCategory>> GetCategoryForMenuAsync();
        Task<IEnumerable<ProductCategory>> GetCategoryOnMainPageAsync();

    }
}
