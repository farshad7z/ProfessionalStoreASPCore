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
        Task<IEnumerable<ProductCategory>> GetAll();
       Task<IEnumerable<ProductCategory>> GetCategoryForMenu();
        Task<IEnumerable<ProductCategory>> GetCategoryOnMainPage();

    }
}
