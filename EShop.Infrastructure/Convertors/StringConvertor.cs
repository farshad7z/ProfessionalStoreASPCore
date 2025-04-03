using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace EShop.Infrastructure.Convertors
{
    public static class StringConvertor
    {
        private static readonly Lazy<Dictionary<string, string>> _persianToEnglishDictionary = new(() =>
        {
            string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (!Directory.Exists(wwwRootPath))
                wwwRootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot");

            string filePath = Path.Combine(wwwRootPath, "Resources", "PersianToEnglish_dictionary.json");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("فایل دیکشنری پیدا نشد!", filePath);

            string jsonContent = File.ReadAllText(filePath);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent) ?? new();

            if (dictionary.Count == 0)
                throw new InvalidOperationException("دیکشنری بارگذاری نشده یا خالی است.");

            return dictionary;
        });

        /// <summary>
        /// تبدیل متن فارسی به معادل انگلیسی بر اساس دیکشنری
        /// </summary>
        private static string PersianToEnglish(string persianText)
        {
            return _persianToEnglishDictionary.Value.TryGetValue(persianText, out var english) ? english : persianText;
        }

        /// <summary>
        /// تبدیل متن فارسی به معادل انگلیسی یا لاتین
        /// </summary>
        public static string PersianToLatinOrEnglish(string persianText)
        {
            var englishText = PersianToEnglish(persianText);
            return (englishText == persianText) ? PersianToLatinMap(persianText) : englishText;
        }

        /// <summary>
        /// تبدیل حروف فارسی به معادل‌های لاتین
        /// </summary>
        private static string PersianToLatinMap(string persianText)
        {
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

            var latinText = new StringBuilder();
            foreach (var character in persianText)
            {
                latinText.Append(persianToLatinMap.TryGetValue(character, out var latin) ? latin : character.ToString());
            }
            return latinText.ToString();
        }
    }
}
