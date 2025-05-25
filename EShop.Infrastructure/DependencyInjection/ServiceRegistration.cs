using EShop.Core.Interfaces.Repositories;
using EShop.Core.Interfaces.Services;
using EShop.Core.Interfaces.UnitOfWork;
using EShop.DAL.Repositories;
using EShop.DAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using EShop.BLL.Services.Public;
using EShop.Core.Interfaces.Services.Public;
using EShop.BLL.Services;
using EShop.Core.Interfaces.Services.Site;
using EShop.BLL.Services.Site;

namespace EShop.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {


            #region Submit Repository Services
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            #endregion

            #region Submit Repository Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion



            #region Submit Bll Services
            //Site
            services.AddScoped<ISiteProductServices, SiteProductServices>();



            //Public
            services.AddScoped<IVariantServices, VariantServices>();
            services.AddScoped<IFeatureService, FeatureServices>();
            services.AddScoped<IProductSEOService, ProductSEOService>();
            services.AddScoped<IProductGalleryServices, ProductGalleryServices>();
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IProductCategoryServices, ProductCategoryServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IPublicServices, PublicServices>();
            
            //services.AddScoped<ICategoryService, CategoryService>();
            #endregion


            return services;
        }
    }
}
