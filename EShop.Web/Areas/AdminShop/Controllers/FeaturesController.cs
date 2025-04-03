using EShop.Core.Entities.Models.Products;
using EShop.Core.Interfaces;
using EShop.Core.Interfaces.Services.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


[Area("AdminShop")]
[Authorize, Authorize(AuthenticationSchemes = "AdminAuth")]
public class FeaturesController : Controller
{
    private readonly IFeatureService _featureService;

    public FeaturesController(IFeatureService featureService)
    {
        _featureService = featureService;
    }

    public async Task<IActionResult> Index()
    {
        var features = await _featureService.GetAllFeaturesAsync();
        return View(features);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Feature feature)
    {
        if (ModelState.IsValid)
        {
            await _featureService.AddFeatureAsync(feature);
            return RedirectToAction("Index");
        }
        return View(feature);
    }

    public IActionResult Edit(int id)
    {
        var feature = _featureService.GetFeatureByIdAsync(id);
        if (feature == null)
        {
            return NotFound();
        }
        return View(feature);
    }

    [HttpPost]
    public IActionResult Edit(Feature feature)
    {
        if (ModelState.IsValid)
        {
            _featureService.UpdateFeatureAsync(feature);
            return RedirectToAction("Index");
        }
        return View(feature);
    }

    //public IActionResult Delete(int id)
    //{
    //    var feature = _featureService.GetFeatureByIdAsync(id);
    //    if (feature == null)
    //    {
    //        return NotFound();
    //    }
    //    _featureService.DeleteFeature(id);
    //    return RedirectToAction("Index");
    //}
}
