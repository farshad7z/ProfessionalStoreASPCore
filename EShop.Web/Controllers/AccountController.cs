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
using System.Security.Claims;
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
        public IActionResult LoginRegister(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

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
                if (isLogin)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl); // بازگشت به صفحه‌ی قبلی
                    }

                    return RedirectToAction("Index", "Home"); // اگر returnUrl نداشت، به صفحه اصلی برمی‌گردد
                }

                ModelState.AddModelError(string.Empty, "ایمیل یا رمز عبور اشتباه است.");
                return View(model);

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
                return Redirect(returnUrl); // بازگشت به صفحه‌ای که کاربر درخواست کرده بود
            }

            return View(model);
        }


        [Authorize(AuthenticationSchemes = "UserAuth")]
        public async Task<IActionResult> LogoutUser()
        {
            await _authenticationService.SignOutUser(HttpContext);
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> LoginAdmin(string password, string dashboard = null)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                // اگر وارد سیستم شده است، به صفحه اصلی هدایت می‌شود
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            if (password!= null)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return RedirectToAction("Index", "Home");

                }
                int userId =userIdClaim.Value !=null ? int.Parse(userIdClaim.Value) : 0;
                var user = await _accountServices.GetUserByIdAsync(userId);
                if (user == null)
                {
                    ModelState.AddModelError("PublicError","  کاربری با این اطلاعات یافت نشد.");
                    return RedirectToAction("Index", "Home");
                }
                bool isExistsEmployee = await _accountServices.IsExistsEmployeeByUserIdAsync(userId);

                if (isExistsEmployee==false)
                {
                    if (dashboard.ToLower()=="adminshop")
                    user.IsEmployeeShop = false;
                    else
                    {
                        user.IsEmployeeSite = false;
                    }
                    await _accountServices.UpdateUserAsync(user);
                    ModelState.AddModelError("PublicError","کارمندی با این اطلاعات یافت نشد.");
                    return RedirectToAction("Index", "Home");
                }

                var employee = await _accountServices.GetDetailsEmployeeByUserIdAsync(userId);
                if (!employee.IsActive)
                {
                    ModelState.AddModelError("PublicError", "حساب کارمندی شما غیرفعال است.");
                    return RedirectToAction("Index", "Home");
                }

                bool isLogin = await loginAdmin( user, employee, true);
                if (isLogin)
                {
                    if (!string.IsNullOrEmpty(dashboard))
                    {
                        if(dashboard.ToLower()== "adminshop")
                        {
                            return Redirect("/AdminShop/Dashboard/Index");

                        }
                        else if (dashboard.ToLower() == "adminsite")
                        {

                        }

                    }
                    return RedirectToAction("Index", "Home"); // اگر returnUrl نداشت، به صفحه اصلی برمی‌گردد

                }
            }
            ModelState.AddModelError(string.Empty, " رمز عبور اشتباه است.");
            return RedirectToAction("Index", "Home"); // اگر returnUrl نداشت، به صفحه اصلی برمی‌گردد
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


        #region Functions

        private async Task<bool> login(User user, bool rememberMe)
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


        private async Task<bool> loginAdmin(User user, Employee employee, bool rememberMe)
        {
            try
            {
                // ورود موفق - ایجاد سشن کاربر
                await _authenticationService.SignInAdmin(HttpContext, user,employee, rememberMe);
                employee.UpdatedAt = DateTime.Now;
                await _accountServices.UpdateEmployeeAsync(employee);
                return true;
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return false;
            }
        }
        #endregion

    }
}
