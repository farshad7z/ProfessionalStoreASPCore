using EShop.Core.DTOs.ViewModels.Product;
using EShop.Core.Entities.Enums;
using EShop.Core.Entities.Models;
using EShop.Core.Interfaces.Services.Site;
using EShop.Core.Interfaces.UnitOfWork;
using EShop.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EShop.BLL.Services.Site
{
    public class SiteProductServices : ISiteProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public SiteProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductViewModel>> GetFilteredProductsAsync(ProductFilterType filterType, string searchTerm = "")
        {
            // دریافت محصولات به‌صورت IQueryable
            var query = await _unitOfWork.Repository<Product>()
                .GetAllAsQueryable(includeProperties: "ProductSelectCategory.ProductCategory");

            // اعمال فیلترها
            switch (filterType)
            {
                case ProductFilterType.Latest:
                    query = query.OrderByDescending(p => p.CreatedAt).Take(10);
                    break;
                case ProductFilterType.MostSold:
                    // در صورتی که معیار "MostSold" را اعمال می‌کنید، باید از SalesCount استفاده کنید.
                    //query = query.OrderByDescending(p => p.SalesCount).Take(10);
                    query = query.Take(10);

                    break;
                case ProductFilterType.MostViewed:
                    // در صورتی که معیار "MostViewed" را اعمال می‌کنید، باید از ViewCount استفاده کنید.
                    //query = query.OrderByDescending(p => p.ViewCount).Take(10);
                    query = query.Take(10);

                    break;
                case ProductFilterType.Search:
                    // فیلتر جستجو
                    query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
                    break;
            }

            // تبدیل به ViewModel و بازگشت نتایج
            var result = await query.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = "~/uploads/products/thumbnail/thumbnail_" + p.ProductImageName,
                Price = p.Price,
                // لیست دسته‌بندی‌ها به جای یک رشته
                CategoryNames = p.ProductSelectCategory.Select(pc => pc.ProductCategory.Name).ToList(),
                IsNew = p.CreatedAt > DateTime.Now.AddMonths(-1), // فرض بر این است که محصولات جدیدتر از یک ماه باید به عنوان "جدید" نمایش داده شوند.
                IsBestSeller = false,  /*p.SalesCount > 50*/ // این مقدار را می‌توانید به دلخواه تغییر دهید.
                IsTopFeatured = true /*p.IsFeatured*/ // فرض بر این است که IsFeatured یک فیلد boolean در مدل محصول باشد.
            }).ToListAsync();

            return result;
        }
        public async Task<ProductDetailViewModel> GetProductByIdAsync(int productId)
        {
            var query = await _unitOfWork.Repository<Product>()
               .GetAllAsQueryable(includeProperties: "ProductSelectCategory.ProductCategory,Galleries,ProductSEO");


            var product = await query.FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                return new ProductDetailViewModel();

            return new ProductDetailViewModel
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ProductImageName,
                Price = product.Price,
                Description = product.Description, // نمایش توضیحات محصول
                CategoryNames = product.ProductSelectCategory?
                    .Where(pc => pc.ProductCategory != null)
                    .Select(pc => pc.ProductCategory!.Name)
                    .ToList() ?? new List<string>(),

                GalleryImages = product.Galleries?
                    .Select(gi => new ProductGalleryViewModel
                    {
                        Title = gi.Title,
                        ImageName = gi.ImageName
                    })
                    .ToList() ?? new List<ProductGalleryViewModel>(),

                MetaKeywords = product.ProductSEO?.MetaKeywords, // دریافت کلمات کلیدی
                IsNew = product.CreatedAt > DateTime.UtcNow.AddDays(-30),
                IsBestSeller = false /*product.SalesCount > 100*/, // فرضی: اگر فروش بیش از ۱۰۰ باشد، پر فروش است
                IsTopFeatured = true /*product.IsFeatured*/
            };
        }
    }


}