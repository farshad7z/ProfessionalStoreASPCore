using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Convertors
{
    public static class StringConvertor
    {
        /// <summary>
        ///تابع برای تبدیل کاراکترهای فارسی به معادل‌های لاتین (تبدیل کاراکترهای فارسی به حروف لاتین)
        /// </summary>
        /// <param name="متن فارسی"></param>
        /// <returns></returns>
        public static string PersianToLatinMap(string persianText)
        {
            // دیکشنری برای تبدیل کاراکترهای فارسی به معادل‌های لاتین
            var persianToLatinMap = new Dictionary<char, string>
            {
                { 'آ', "a" }, { 'ا', "a" }, { 'ب', "b" }, { 'پ', "p" }, { 'ت', "t" },
                { 'ث', "th" }, { 'ج', "j" }, { 'چ', "ch" }, { 'ح', "h" }, { 'خ', "kh" },
                { 'د', "d" }, { 'ذ', "z" }, { 'ر', "r" }, { 'ز', "z" }, { 'ژ', "zh" },
                { 'س', "s" }, { 'ش', "sh" }, { 'ص', "s" }, { 'ض', "z" }, { 'ط', "t" },
                { 'ظ', "z" }, { 'ع', "a" }, { 'غ', "gh" }, { 'ف', "f" }, { 'ق', "gh" },
                { 'ک', "k" }, { 'گ', "g" }, { 'ل', "l" }, { 'م', "m" }, { 'ن', "n" },
                { 'و', "v" }, { 'ه', "h" }, { 'ی', "y" }
            };

            // تبدیل متن فارسی به معادل‌های لاتین
            var latinText = new StringBuilder();
            foreach (var character in persianText)
            {
                if (persianToLatinMap.ContainsKey(character))
                {
                    latinText.Append(persianToLatinMap[character]);
                }
                else
                {
                    latinText.Append(character);
                }
            }

            return latinText.ToString();
        }
    }

}
