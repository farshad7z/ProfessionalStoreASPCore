using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EShop.Core.Entities.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EShop.Infrastructure.Security
{
    public class AuthenticationService
    {
        private readonly TimeSpan _userAuthExpireTimeSpan = TimeSpan.FromDays(7); // مدت زمان انقضا برای کاربران
        private readonly TimeSpan _adminAuthExpireTimeSpan = TimeSpan.FromHours(12); // مدت زمان انقضا برای ادمین‌ها
                                                                                     // تعریف ILogger به صورت generic برای کلاس AuthenticationService
        private readonly ILogger<AuthenticationService> _logger;

        // دریافت ILogger از طریق سازنده
        public AuthenticationService(ILogger<AuthenticationService> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Sign in a regular user.
        /// </summary>
        public async Task SignInUser(HttpContext httpContext, User user, bool rememberMe)
        {
            if (!user.IsActive)
                throw new InvalidOperationException("حساب کاربری فعال نیست.");

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim("IsActive", user.IsActive.ToString().ToLower()),
            new Claim("InitialAuth", "true"), // Indicates initial authentication
            new Claim("IsEmployeeShop", user.IsEmployeeShop.ToString().ToLower()),
            new Claim("IsEmployeeSite", user.IsEmployeeSite.ToString().ToLower()),

        };

            var claimsIdentity = new ClaimsIdentity(claims, "UserAuth");

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = rememberMe ? DateTime.UtcNow.Add(_userAuthExpireTimeSpan) : DateTime.UtcNow.AddHours(2),
                AllowRefresh = true
            };

            await httpContext.SignInAsync("UserAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

            // بررسی مقدار `AuthenticationType`
            Console.WriteLine($"[AUTH DEBUG] User.Identity.AuthenticationType: {httpContext.User.Identity.AuthenticationType}");
            Console.WriteLine($"[AUTH DEBUG] User.Identity.IsAuthenticated: {httpContext.User.Identity.IsAuthenticated}");

            _logger.LogInformation("[AUTH DEBUG] User.Identity.AuthenticationType: {AuthType}", httpContext.User.Identity.AuthenticationType);
            _logger.LogInformation("[AUTH DEBUG] User.Identity.IsAuthenticated: {IsAuthenticated}", httpContext.User.Identity.IsAuthenticated);
            _logger.LogInformation("User {UserId} signed in successfully.", user.UserId);

            Console.WriteLine($"User {user.UserId} signed in successfully.");
        }

        /// <summary>
        /// Sign in an admin user.
        /// </summary>
        public async Task SignInAdmin(HttpContext httpContext, User user)
        {
            if (!user.IsActive)
                throw new InvalidOperationException("حساب کاربری فعال نیست.");

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim("AdminAuth", "true") // Indicates admin authentication
        };

            if (user.UserRoles != null)
            {
                foreach (var role in user.UserRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Role.RoleName));
                }
            }

            if (user.UserClaims != null)
            {
                foreach (var userClaim in user.UserClaims)
                {
                    claims.Add(new Claim(userClaim.Claim.ClaimType, userClaim.ClaimValue));
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, "AdminAuth");

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.Add(_adminAuthExpireTimeSpan),
                AllowRefresh = true
            };

            await httpContext.SignInAsync("AdminAuth", new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        /// <summary>
        /// Sign out the user.
        /// </summary>
        public async Task SignOutUser(HttpContext httpContext)
        {
            await httpContext.SignOutAsync("UserAuth");
            await httpContext.SignOutAsync("AdminAuth");

            // Redirect to login page after sign-out
            httpContext.Response.Redirect("/Account/Login");
        }
    }

}