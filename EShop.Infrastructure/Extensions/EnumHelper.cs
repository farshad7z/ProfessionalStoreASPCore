using System.Reflection;
using System.ComponentModel;
using EShop.Core.Entities.Enums;
using EShop.Core.DTOs.ViewModels.Admin.Product;
using EShop.Infrastructure.Extensions;

public static class EnumHelper
{
    public static List<ColorItem> GetProductColorItems<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
                   .Cast<TEnum>()
                   .Select(e => new ColorItem
                   {
                       Name = e.ToString(),                            
                       Title = $"{EnumExtensions.GetEnumDescription(e)} - {e}", 
                       ColorCode = $"#{Convert.ToInt32(e):X6}"            
                   })
                   .ToList();
    }
}

