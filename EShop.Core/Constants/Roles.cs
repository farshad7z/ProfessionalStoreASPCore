using EShop.Core.Entities.Models;

namespace OnlineStore.Core.Constants
{
    public static class Roles
    {
        public const string SuperAdmin = "SuperAdmin"; // مدیر اصلی سایت
        public const string Admin = "Admin";      // مدیر سایت
        public const string ShopOwner = "ShopOwner";    // فروشنده که یک فروشگاه دارد
        public const string ShopManager = "ShopManager";
        public const string Customer = "Customer"; // کاربر عادی که خرید می‌کند
        public const string Moderator = "Moderator"; // مدیر محتوای سایت
        public const string DeliveryAgent = "DeliveryAgent";
    }
}
