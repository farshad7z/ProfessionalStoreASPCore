using EShop.BLL.Services;
using EShop.BLL.Services.Site;
using EShop.Core.DTOs.ViewModels.Product;
using EShop.Core.Entities.Enums;
using EShop.Core.Enums;
using EShop.Core.Interfaces.Services.Site;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductListViewComponent : ViewComponent
{
    private readonly ISiteProductServices _productService;

    public ProductListViewComponent(ISiteProductServices productService)
    {
        _productService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync(ProductFilterType filterType, string searchTerm = "",bool isRequestFormIndex=true)
    {
        if (isRequestFormIndex)
        {
        var products = await _productService.GetFilteredProductsAsync(filterType, searchTerm);
        return View("/Components/ProductList/ProductListView.cshtml", products);
        }
        else
        {
            var products = await _productService.GetFilteredProductsAsync(filterType, searchTerm);
            return View("/Components/ProductList/ProductOnShopListView.cshtml", products);
        }

    }
}
