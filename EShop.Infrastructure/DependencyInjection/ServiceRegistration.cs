using EShop.Core.Interfaces.Repositories;
using EShop.Core.Interfaces.Services;
using EShop.Core.Interfaces.UnitOfWork;
using EShop.DAL.Repositories;
using EShop.DAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using EShop.BLL.Services.Public;
using EShop.Core.Interfaces.Services.Public;

namespace EShop.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            #region Submit Bll Services
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IProductCategoryServices, ProductCategoryServices>();

            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<ICategoryService, CategoryService>();
            #endregion

            #region Submit Repository Services
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            #endregion

            #region Submit Repository Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            return services;
        }
    }
}
