using EShop.Core.Interfaces.Services.Public;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Components
{
    public class MegaMenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryServices _productCategoryServices;
        public MegaMenuViewComponent(IProductCategoryServices productCategoryServices)
        {
            _productCategoryServices = productCategoryServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _productCategoryServices.GetCategoryForMenu();
            return View(result);

        }
    }
}
