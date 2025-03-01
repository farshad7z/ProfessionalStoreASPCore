namespace EShop.Core.Constants
{
    public static class ConstClaims
    {
        // کلایم‌های عمومی
        public const string UserId = "UserId"; // شناسه کاربر
        public const string PhoneNumber = "PhoneNumber"; // شماره تماس کاربر
        public const string FullName = "FullName"; // نام کامل کاربر
        public const string Email = "Email"; // ایمیل کاربر
        public const string Role = "Role"; // نقش کاربر
        public const string IsActive = "IsActive"; // آیا حساب فعال است؟
        public const string HasShop = "HasShop"; // آیا کاربر فروشگاه دارد؟
        public const string ShopId = "ShopId"; // آی‌دی فروشگاه کاربر



        // کلایم‌های مدیریتی برای سایت
        public const string AdminCanManageShops = "AdminCanManageShops";
        public const string AdminCanEditShops = "AdminCanEditShops";
        public const string AdminCanRemoveShops = "AdminCanRemoveShops";
        public const string AdminCanEditProducts = "AdminCanEditProducts";
        public const string AdminCanRemoveProducts = "AdminCanRemoveProducts";
        public const string AdminCanManageOrders = "AdminCanManageOrders";
        public const string AdminCanAddUsers = "AdminCanAddUsers";
        public const string AdminCanEditUsers = "AdminCanEditUsers";
        public const string AdminCanRemoveUsers = "AdminCanRemoveUsers";
        public const string AdminCanReplayComments = "AdminCanReplayComments";
        public const string AdminManageAccessUsers = "AdminManageAccessUsers";



        // کلایم‌های مدیریتی برای فروشگاه
        public const string CanManageShop = "CanManageShop"; // دسترسی مدیریت فروشگاه
        public const string CanEditProducts = "CanEditProducts"; // دسترسی ویرایش محصولات
        public const string CanAddProducts = "CanAddProducts"; // دسترسی افزودن محصولات
        public const string CanRemoveProducts = "CanRemoveProducts"; // دسترسی حذف محصولات
        public const string CanManageOrders = "CanManageOrders"; // دسترسی مدیریت سفارشات
        public const string CanManageContent = "CanManageContent"; // دسترسی مدیریت محتوا
        public const string CanManageUsers = "CanManageUsers";
        public const string CanAddUsersOnShop = "CanAddUsersOnShop";
        public const string CanRemoveUsersOnShop = "CanRemoveUsersOnShop";
        public const string ManageAccessUsers = "AdminManageAccessUsers";

    }
}
