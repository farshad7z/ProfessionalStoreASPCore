using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EShop.Core.Validators
{public class PhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return new ValidationResult("شماره موبایل الزامی است.");
        }

        string phoneNumber = value.ToString();
        string pattern = @"^09\d{9}$"; // شماره موبایل ایران (مانند: 09123456789)

        if (!Regex.IsMatch(phoneNumber, pattern))
        {
            return new ValidationResult("شماره موبایل نامعتبر است.");
        }

        return ValidationResult.Success;
    }
}
}