using EShop.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Infrastructure.Security
{
    public static class PolicyRegistration
    {
        public static void AddCustomPolicies(this IServiceCollection services)
        {
            services.AddAuthorizationCore(options =>
            {
                // ✅ سیاست دسترسی فقط برای SuperAdmin
                options.AddPolicy(Policies.RequireSuperAdmin, policy =>
                    policy.RequireRole(Roles.SuperAdmin));

                // ✅ سیاست دسترسی فقط برای Admin و SuperAdmin
                options.AddPolicy(Policies.RequireAdmin, policy =>
                    policy.RequireRole(Roles.Admin, Roles.SuperAdmin));

                // ✅ سیاست دسترسی برای مالک فروشگاه
                options.AddPolicy(Policies.RequireShopOwner, policy =>
                    policy.RequireClaim(ConstClaims.HasShop, "true")
                           .RequireRole(Roles.ShopOwner));

                // ✅ سیاست دسترسی برای مدیران فروشگاه (ShopManager)
                options.AddPolicy(Policies.RequireShopManager, policy =>
                    policy.RequireClaim(ConstClaims.CanManageShop, "true")
                           .RequireRole(Roles.ShopManager, Roles.ShopOwner));

                // ✅ سیاست دسترسی برای ویرایش محصولات
                options.AddPolicy(Policies.RequireProductManagement, policy =>
                    policy.RequireClaim(ConstClaims.CanEditProducts, "true"));

                // ✅ سیاست دسترسی برای مدیریت کاربران
                options.AddPolicy(Policies.RequireUserManagement, policy =>
                    policy.RequireRole(Roles.SuperAdmin,ConstClaims.CanManageUsers,"True"));


                // ✅ سیاست دسترسی برای اضافه کردن کاربران
                options.AddPolicy(Policies.RequireAddUserOnshop, policy =>
                    policy.RequireClaim(ConstClaims.CanAddUsersOnShop, "True"));
                // ✅ سیاست دسترسی برای اضافه کردن کاربران
                options.AddPolicy(Policies.RequireRemoveUserOnshop, policy =>
                    policy.RequireClaim(ConstClaims.CanRemoveUsersOnShop, "True"));


                // ✅ سیاست دسترسی برای مدیریت سفارش‌ها
                options.AddPolicy(Policies.RequireOrderManagement, policy =>
                    policy.RequireClaim(ConstClaims.CanManageOrders, "true"));

                // ✅ سیاست دسترسی برای کاربران فعال
                options.AddPolicy(Policies.RequireActiveUser, policy =>
                    policy.RequireClaim(ConstClaims.IsActive, "true"));

                // ✅ سیاست برای کاربران عادی (مشتری‌ها)
                options.AddPolicy(Policies.RequireCustomer, policy =>
                    policy.RequireRole(Roles.Customer));

                // ✅ سیاست برای مدیر محتوای سایت (Moderator)
                options.AddPolicy(Policies.RequireModerator, policy =>
                    policy.RequireRole(Roles.Moderator));

                // ✅ سیاست برای مأموران ارسال کالا (DeliveryAgent)
                options.AddPolicy(Policies.RequireDeliveryAgent, policy =>
                    policy.RequireRole(Roles.DeliveryAgent));
            });
        }
    }
}
