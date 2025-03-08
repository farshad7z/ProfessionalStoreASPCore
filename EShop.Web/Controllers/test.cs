using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Web.Controllers
{
    public class TestController : Controller
    {
       
            [Route("Test")]
            public IActionResult test()
            {

            bool isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            string authScheme = User.Identities.FirstOrDefault()?.AuthenticationType; // UserAuth یا AdminAuth
            if (isAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var firstName = User.FindFirst(ClaimTypes.Name)?.Value;

                return Content($"کاربر {firstName} با شناسه {userId} وارد شده است.");
            }

            return Content("کاربر وارد سیستم نشده است.");
        }

        [Route("TestUserAuth")]
        [Authorize(AuthenticationSchemes = "UserAuth")]
        public IActionResult testUserAuth()
        {
            bool isAuthenticated = HttpContext.User.Identity.IsAuthenticated;

            if (isAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var firstName = User.FindFirst(ClaimTypes.GivenName)?.Value;
                return Content($"کاربر {firstName} با شناسه {userId} وارد شده است.");
            }

            return Content("کاربر وارد سیستم نشده است.");
        }


    }
}
