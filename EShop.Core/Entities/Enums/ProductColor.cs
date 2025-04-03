using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Core.Entities.Enums
{
    public enum ProductColor
    {
        // رنگ‌های اصلی
        Red = 0xFF0000,
        Green = 0x008000,
        Blue = 0x0000FF,
        Black = 0x000000,
        White = 0xFFFFFF,
        Yellow = 0xFFFF00,
        Orange = 0xFFA500,
        Purple = 0x800080,
        Pink = 0xFFC0CB,
        Gray = 0x808080,

        // رنگ‌های اضافی
        Cyan = 0x00FFFF,
        Magenta = 0xFF00FF,
        Brown = 0xA52A2A,
        Lime = 0x00FF00,
        Navy = 0x000080,
        Olive = 0x808000,
        Teal = 0x008080,
        Maroon = 0x800000,
        Silver = 0xC0C0C0,
        Gold = 0xFFD700,

        // رنگ‌های خاص
        LightBlue = 0xADD8E6,
        LightGreen = 0x90EE90,
        LightGray = 0xD3D3D3,
        DarkBlue = 0x00008B,
        DarkGreen = 0x006400,
        DarkGray = 0xA9A9A9,
        DarkRed = 0x8B0000,
        DarkOrange = 0xFF8C00,
        DarkCyan = 0x008B8B,
        DarkMagenta = 0x8B008B,

        // رنگ‌های خاص‌تر
        Peach = 0xFFE5B4,
        Lavender = 0xE6E6FA,
        Coral = 0xFF7F50,
        Beige = 0xF5F5DC,
        Turquoise = 0x40E0D0,
        Violet = 0xEE82EE,
        Salmon = 0xFA8072,
        Indigo = 0x4B0082,
        Chocolate = 0xD2691E,
        Crimson = 0xDC143C
    }

}
