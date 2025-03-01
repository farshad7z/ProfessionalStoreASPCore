using EShop.Core.Constants;
using EShop.Core.Entities.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EShop.Infrastructure.Security
{
    public  class AuthenticationService
    {
        public  async Task SignInUser(HttpContext httpContext, User user)
        {
            if (!user.IsActive)
            {
                throw new InvalidOperationException("حساب کاربری فعال نیست.");
            }

            var claims = new List<Claim>
            {
                new Claim(ConstClaims.UserId, user.UserId.ToString()),
                new Claim(ConstClaims.FullName, $"{user.FirstName} {user.LastName}"),
                new Claim(ConstClaims.PhoneNumber, user.PhoneNumber),
                new Claim(ConstClaims.IsActive, user.IsActive.ToString().ToLower()),
                new Claim(ConstClaims.Email, user.Email??""),
                new Claim(ConstClaims.HasShop, user.HasShop.ToString().ToLower()),

                // نقش‌ها
                new Claim(ConstClaims.Role, string.Join(",", user.UserRoles?.Select(ur => ur.Role?.RoleName) ?? new string[] { "DefaultRole" }))
            };

            // اضافه کردن کلایم‌های اختصاصی
            if (user.UserClaims != null)
            {
                foreach (var userClaim in user.UserClaims)
                {
                    claims.Add(new Claim(userClaim.Claim.ClaimType, userClaim.ClaimValue));
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7) // 7 days
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public  async Task SignOutUser(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

    }
}