using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Enums
{
    public enum FeatureType
    {
        [Display(Name = "متنی")]
        Text = 1,

        [Display(Name = "عددی")]
        Number = 2,

        [Display(Name = "بازه‌ای")]
        Range = 3,

        [Display(Name = "تاریخ")]
        Date = 4,

        [Display(Name = "بلی/خیر")]
        Boolean = 5,

        [Display(Name = "رنگ")]
        Color = 6
    }
    // برای مقادیر متنی (مثل رنگ: "قرمز")
    // برای مقادیر عددی (مثل "رم: 8GB")
    // برای فیلترهای بازه‌ای (مثل قیمت: 100000-500000)
    // برای ویژگی‌های تاریخ (مثل تاریخ تولید: "2022-05-15")
    // برای ویژگی‌های بلی/خیر (مثل "گارانتی موجود است؟")
    // برای مقادیر رنگ (مثل "قرمز", "آبی")

}
