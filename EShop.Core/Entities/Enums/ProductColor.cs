using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Enums
{
    using System.ComponentModel;

    public enum ProductColor
    {
        // رنگ‌های اصلی
        [Description("قرمز - Red")]
        Red = 0xFF0000,

        [Description("سبز - Green")]
        Green = 0x008000,

        [Description("آبی - Blue")]
        Blue = 0x0000FF,

        [Description("مشکی - Black")]
        Black = 0x000000,

        [Description("سفید - White")]
        White = 0xFFFFFF,

        [Description("زرد - Yellow")]
        Yellow = 0xFFFF00,

        [Description("نارنجی - Orange")]
        Orange = 0xFFA500,

        [Description("بنفش - Purple")]
        Purple = 0x800080,

        [Description("صورتی - Pink")]
        Pink = 0xFFC0CB,

        [Description("خاکستری - Gray")]
        Gray = 0x808080,

        [Description("فیروزه‌ای - Cyan")]
        Cyan = 0x00FFFF,

        [Description("ارغوانی - Magenta")]
        Magenta = 0xFF00FF,

        [Description("قهوه‌ای - Brown")]
        Brown = 0xA52A2A,

        [Description("لیمویی - Lime")]
        Lime = 0x00FF00,

        [Description("سرمه‌ای - Navy")]
        Navy = 0x000080,

        [Description("زیتونی - Olive")]
        Olive = 0x808000,

        // رنگ‌های تیره
        [Description("آبی تیره - Dark Blue")]
        DarkBlue = 0x00008B,

        [Description("سبز تیره - Dark Green")]
        DarkGreen = 0x006400,

        [Description("قرمز تیره - Dark Red")]
        DarkRed = 0x8B0000,

        [Description("نارنجی تیره - Dark Orange")]
        DarkOrange = 0xFF8C00,

        // رنگ‌های روشن
        [Description("آبی روشن - Light Blue")]
        LightBlue = 0xADD8E6,

        [Description("صورتی روشن - Light Pink")]
        LightPink = 0xFFB6C1,

        [Description("سبز روشن - Light Green")]
        LightGreen = 0x90EE90,

        // رنگ‌های متالیک و خاص
        [Description("طلایی - Gold")]
        Gold = 0xFFD700,

        [Description("نقره‌ای - Silver")]
        Silver = 0xC0C0C0,

        [Description("برنزی - Bronze")]
        Bronze = 0xCD7F32,

        [Description("مسیرنگ - Copper")]
        Copper = 0xB87333,

        // رنگ‌های طبیعت
        [Description("آبی آسمانی - Sky Blue")]
        SkyBlue = 0x87CEEB,

        [Description("سبز جنگلی - Forest Green")]
        ForestGreen = 0x228B22,

        [Description("خاکی - Khaki")]
        Khaki = 0xF0E68C,

        // رنگ‌های خاص دیگر
        [Description("مرجانی - Coral")]
        Coral = 0xFF7F50,

        [Description("یاسی - Lavender")]
        Lavender = 0xE6E6FA,

        [Description("گلبهی - Peach")]
        Peach = 0xFFE5B4,

        [Description("فیروزه‌ای - Turquoise")]
        Turquoise = 0x40E0D0,

        [Description("شکلاتی - Chocolate")]
        Chocolate = 0xD2691E,

        [Description("ارغوانی پررنگ - Indigo")]
        Indigo = 0x4B0082,

        [Description("زرشکی - Crimson")]
        Crimson = 0xDC143C,

        [Description("عنابی - Maroon")]
        Maroon = 0x800000,

        [Description("زرد لیمویی - Lemon")]
        Lemon = 0xFFF700,

        [Description("بنفش یاسی - Lilac")]
        Lilac = 0xC8A2C8
    }
}
