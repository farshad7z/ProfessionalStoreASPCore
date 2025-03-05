using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EShop.Core.Entities.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace EShop.Infrastructure.Security
{
    public class AuthenticationService
    {
        /// <summary>
        /// Signs in a regular user.
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
        new Claim("HasShop", user.HasShop.ToString().ToLower()),
    };

            var claimsIdentity = new ClaimsIdentity(claims, "UserAuth");

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = rememberMe ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddHours(2),
                AllowRefresh = true // Optional: Allows token refresh
            };

            await httpContext.SignInAsync("UserAuth", new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        /// <summary>
        /// Signs in an admin user.
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

            // Add roles and custom claims
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
                AllowRefresh = true // Optional: Allows token refresh
            };

            await httpContext.SignInAsync("AdminAuth", new ClaimsPrincipal(claimsIdentity), authProperties);
        }
        /// <summary>
        /// Signs out the user.
        /// </summary>
        public async Task SignOutUser(HttpContext httpContext)
        {
            await httpContext.SignOutAsync("UserAuth");
            await httpContext.SignOutAsync("AdminAuth");
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to login page after sign-out
            httpContext.Response.Redirect("/Account/Login");
        }
    }
}