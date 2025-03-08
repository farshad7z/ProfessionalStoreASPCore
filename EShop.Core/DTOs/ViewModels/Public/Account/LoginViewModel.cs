using System.ComponentModel.DataAnnotations;

public class LoginRegisterViewModel
{
    public LoginViewModel? Login { get; set; }
    public RegisterViewModel? Register { get; set; }
}

public class LoginViewModel
{
    [Display(Name = "شماره موبایل")]
    [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
    [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل معتبر نیست.")]
    public string MobileNumber { get; set; } = string.Empty;  // Empty string for default

    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
    [MinLength(6, ErrorMessage = "رمز عبور نباید کمتر از {1} کاراکتر باشد.")]
    public string Password { get; set; } = string.Empty;  // Empty string for default

    [Display(Name = "مرا به خاطر بسپار")]
    public bool RememberMe { get; set; }
}

public class RegisterViewModel
{
    [Display(Name = "شماره موبایل")]
    [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
    [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل معتبر نیست.")]
    public string MobileNumber { get; set; } = string.Empty;  // Empty string for default

    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
    [MinLength(6, ErrorMessage = "رمز عبور نباید کمتر از {1} کاراکتر باشد.")]
    public string Password { get; set; } = string.Empty;  // Empty string for default

    [Display(Name = "تکرار رمز عبور")]
    [Required(ErrorMessage = "لطفاً {0} را وارد کنید.")]
    [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نیستند.")]
    public string ConfirmPassword { get; set; } = string.Empty;  // Empty string for default
}
