using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EShop.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// دریافت نام نمایشی (Display Name) مقدار `Enum`
        /// </summary>
        /// <param name="value">مقدار `Enum` که باید نام نمایشی آن دریافت شود</param>
        /// <returns>
        /// در صورتی که مقدار `Enum` دارای `DisplayAttribute` باشد، مقدار `Name` آن برگردانده می‌شود،  
        /// در غیر این صورت، مقدار `Enum` به صورت رشته‌ای برگردانده می‌شود.
        /// </returns>
        public static string GetEnumDescription<TEnum>(TEnum value) where TEnum : Enum
        {
            var field = typeof(TEnum).GetField(value.ToString());
            var attr = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                             .FirstOrDefault() as DescriptionAttribute;
            return attr?.Description ?? value.ToString();
        }
    }
}