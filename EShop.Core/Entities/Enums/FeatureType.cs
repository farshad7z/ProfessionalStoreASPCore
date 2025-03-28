using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Enums
{
    public enum FeatureType
    {
        Text = 1,     // برای مقادیر متنی (مثل رنگ: "قرمز")
        Number = 2,   // برای مقادیر عددی (مثل "رم: 8GB")
        Range = 3     // برای فیلترهای بازه‌ای (مثل قیمت: 100000-500000)
    }
}
