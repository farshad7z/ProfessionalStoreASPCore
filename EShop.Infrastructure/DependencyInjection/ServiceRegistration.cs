using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        //public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        //{
        //    // ثبت سرویس‌های BLL
        //    services.AddScoped<IProductService, ProductService>();
        //    services.AddScoped<ICategoryService, CategoryService>();

        //    // ثبت سرویس‌های Repository
        //    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        //    services.AddScoped<IProductRepository, ProductRepository>();
        //    services.AddScoped<ICategoryRepository, CategoryRepository>();

        //    // ثبت Unit of Work
        //    services.AddScoped<IUnitOfWork, UnitOfWork>();

        //    return services;
        //}
    }
}
