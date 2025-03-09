using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using EShop.Core.DTOs.ViewModels.Public.Account;
using EShop.Core.Interfaces.Services;
using EShop.Core.DTOs.ViewModels.Public;
using EShop.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using EShop.Core.Entities.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
namespace EShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly AuthenticationService _authenticationService;
        public AccountController(IAccountServices accountServices, AuthenticationService authenticationService)
        {
            _accountServices = accountServices;
            _authenticationService = authenticationService;
        }


        //[Route("login")]        
        [HttpGet]
        public IActionResult LoginRegister()
        {
            return View(new LoginRegisterViewModel());
        }



        [HttpPost]
        public async Task<IActionResult> LoginRegister(LoginRegisterViewModel model, string returnUrl = null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                // اگر وارد سیستم شده است، به صفحه اصلی هدایت می‌شود
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Login != null)
            {
                // بخش ورود
                if (!Regex.IsMatch(model.Login.MobileNumber, @"^09\d{9}$"))
                {
                    ModelState.AddModelError("Mobile", "شماره موبایل نامعتبر است.");
                    return View(model);
                }

                var user = await _accountServices.GetUserByMobileNumberAsync(model.Login.MobileNumber);

                if (user == null)
                {
                    
                        ModelState.AddModelError("Login.MobileNumber", "کاربری با این شماره یافت نشد.");
                       return View(model);
                }

                if (!user.IsActive)
                {
                    ModelState.AddModelError("Login.MobileNumber", "حساب کاربری شما غیرفعال است.");
                    return View(model);
                }

                bool isLogin = await login(user, model.Login.RememberMe);
                return RedirectToAction("index", "Home");
            }
            else if (model.Register != null)
            {
                // بخش ثبت‌نام
                if (!Regex.IsMatch(model.Register.MobileNumber, @"^09\d{9}$"))
                {
                    ModelState.AddModelError("Register.MobileNumber", "شماره موبایل نامعتبر است.");
                    return View(model);
                }

                var existingUser = await _accountServices.GetUserByMobileNumberAsync(model.Register.MobileNumber);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Register.MobileNumber", "این شماره موبایل قبلاً ثبت‌نام شده است.");
                    return View(model);
                }

                var user = new User
                {
                    PhoneNumber = model.Register.MobileNumber,
                    PasswordHash =PasswordHasher.HashPassword(model.Register.Password),  // رمز عبور باید هش شده باشد
                    IsActive = true,
                    IsEmployeeShop = false,
                    IsEmployeeSite=false,
                    RegistrationDate=DateTime.Now,
                    LastLoginDate=DateTime.Now,
                };

                await _accountServices.RegisterUserAsync(user);
                var result = await _accountServices.GetUserByMobileNumberAsync(user.PhoneNumber);
                bool isLogin= await login(result, false);
                return RedirectToAction("index", "Home");
            }

            return View(model);
        }
        private async Task<bool> login(User user,bool rememberMe)
        {
            try
            {
                // ورود موفق - ایجاد سشن کاربر
                await _authenticationService.SignInUser(HttpContext, user, rememberMe);
                user.LastLoginDate = DateTime.Now;
                await _accountServices.UpdateUserAsync(user);
                return true;
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return false;
            }
        }

        //[HttpPost]
        //    public async Task<IActionResult> RegisterUser(RegisterViewModel model , string returnUrl=null )
        //    {
        //        // بررسی اینکه آیا کاربر قبلاً وارد سیستم شده است
        //        if (HttpContext.User.Identity.IsAuthenticated)
        //        {
        //            // اگر وارد سیستم شده است، به صفحه اصلی یا صفحه پروفایل هدایت می‌کنیم
        //            return RedirectToAction("Index", "Home");  // یا هر صفحه دیگری که می‌خواهید
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            return View("LoginRegister", new LoginRegisterViewModel() { Register = model });
        //        }
        //        if (!Regex.IsMatch(model.MobileNumber, @"^09\d{9}$"))
        //        {
        //            ModelState.AddModelError("Mobile", "شماره موبایل نامعتبر است.");
        //        }
        //        var user = await _accountServices.GetUserByMobileNumberAsync(model.MobileNumber);

        //        if (user != null)
        //        {
        //            ModelState.AddModelError("Mobile", "کاربری با این شماره قبلا  ثبت نام کرده.");
        //            return View("LoginRegister", new LoginRegisterViewModel() { Register = model });
        //        }

        //        if (!user.IsActive)
        //        {
        //            ModelState.AddModelError("Mobile", "حساب کاربری شما غیرفعال است.");
        //            return View("LoginRegister", new LoginRegisterViewModel() { Register = model });
        //        }
        //        if (model.Password !=model.Password)
        //        {
        //            ModelState.AddModelError("Mobile", "تکرار رمز عبور مطابقت ندارد.");
        //            return View("LoginRegister", new LoginRegisterViewModel() { Register = model });
        //        }
        //        model.Password = PasswordHasher.HashPassword(model.Password);
        //        _accountServices.RegisterUserAsync(model);
        //        return RedirectToLocal(returnUrl);

        //    }

        [Authorize(AuthenticationSchemes = "UserAuth")]
        public async Task<IActionResult> LogoutUser()
        {
            await _authenticationService.SignOutUser(HttpContext);
            return RedirectToAction("Index", "Home");

        }
        // GET: AccountController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // این متد بررسی می‌کند که کاربر به صفحه قبلی برگشته یا خیر
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
