using EShop.DAL.Context;
using Microsoft.EntityFrameworkCore;
using EShop.Infrastructure.DependencyInjection;
using EShop.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);



#region
builder.Services.AddDbContext<EShopDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("EShop.DAL") // نام اسمبلی که Migrationها در آن ساخته می‌شود

    ));

#endregion

//// خواندن ConnectionString از appsettings.json
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// اضافه کردن سرویس‌های پروژه
builder.Services.AddApplicationServices();
// ثبت سیاست‌های دسترسی
builder.Services.AddCustomPolicies();

#region Authentication


//تنظیمات احراز هویت (Authentication)
builder.Services.AddAuthentication(options =>
{



    options.DefaultScheme = "UserAuth";
    options.DefaultAuthenticateScheme = "UserAuth";
    options.DefaultSignInScheme = "UserAuth";
    options.DefaultChallengeScheme = "UserAuth";
})
.AddCookie("UserAuth", options =>
{
    options.LoginPath = "/Account/LoginRegister";
    options.AccessDeniedPath = "/Account/LogoutUser";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
})
.AddCookie("AdminAuth", options =>
{
    // تنظیمات زمان انقضای کوکی برای ادمین‌ها
    options.LoginPath = "/Admin/Login";
    options.AccessDeniedPath = "/Admin/AccessDenied";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
    //options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // فقط از طریق HTTPS ارسال شود
    options.ExpireTimeSpan = TimeSpan.FromHours(12); // برای زمان انقضای کوکی 12 ساعت
    options.SlidingExpiration = true; // اجازه تمدید زمان انقضا در صورت دسترسی مجدد
});

#endregion


//// ثبت سرویس‌ها
//builder.Services.AddScoped<AuthenticationService>();
// به این:
//builder.Services.AddScoped<JwtTokenManager>(provider =>
//{
//    var configuration = provider.GetRequiredService<IConfiguration>();
//    return new JwtTokenManager(
//        configuration["Jwt:SecretKey"],
//        configuration["Jwt:Issuer"],
//        configuration["Jwt:Audience"]
//    );
//});
// Add services to the container.
builder.Services.AddControllersWithViews();


// تنظیمات احراز هویت برای کاربران

//builder.Services.AddAuthentication(options =>
//{
//    // در اینجا به صورت پیش‌فرض می‌توانید یک Scheme اصلی تعیین کنید.
//    options.DefaultScheme = "UserAuth";
//    options.DefaultChallengeScheme = "UserAuth";
//})
//.AddCookie("UserAuth", options =>
//{
//    options.LoginPath = "/Account/LoginRegister"; // مسیر لاگین برای کاربران عادی
//    options.LogoutPath = "/Account/LogoutUser"; // مسیر خروج برای کاربران عادی
//    options.ExpireTimeSpan = TimeSpan.FromDays(7);
//    options.SlidingExpiration = true;
//    // تعیین نام کوکی برای راحتی عیب‌یابی
//    options.Cookie.Name = "UserAuthCookie";
//})
//.AddCookie("AdminAuth", options =>
//{
//    options.LoginPath = "/Account/AdminLogin";
//    options.LogoutPath = "/Account/AdminLogout";
//    options.ExpireTimeSpan = TimeSpan.FromHours(12);
//    options.SlidingExpiration = true;
//    options.Cookie.Name = "AdminAuthCookie";
//});


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = "UserAuth";
//    options.DefaultAuthenticateScheme = "UserAuth";
//    options.DefaultSignInScheme = "UserAuth";
//    options.DefaultChallengeScheme = "UserAuth";
//})
//.AddCookie("UserAuth", options =>
//{
//    options.LoginPath = "/Account/LoginRegister";
//    options.AccessDeniedPath = "/Account/LogoutUser";
//    options.Cookie.HttpOnly = true;
//    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
//    options.ExpireTimeSpan = TimeSpan.FromDays(7);
//    options.SlidingExpiration = true;
//})
//.AddCookie("AdminAuth", options =>
//{
//    options.LoginPath = "/Admin/Login";
//    options.AccessDeniedPath = "/Admin/AccessDenied";
//    options.Cookie.HttpOnly = true;
//    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
//    options.ExpireTimeSpan = TimeSpan.FromHours(12);
//    options.SlidingExpiration = true;
//});

//builder.Services.AddAuthentication(options =>
//{
//    // تغییر به "UserAuth" به عنوان اسکیما پیش‌فرض برای کاربران
//    options.DefaultScheme = "UserAuth";
//    options.DefaultAuthenticateScheme = "UserAuth";
//    options.DefaultSignInScheme = "UserAuth";
//    options.DefaultChallengeScheme = "UserAuth";
//})
//    .AddCookie("UserAuth", options =>
//{
//    options.LoginPath = "/Account/LoginRegister";
//    options.AccessDeniedPath = "/Account/LogoutUser";
//    options.Cookie.HttpOnly = true;
//    options.Cookie.SecurePolicy = CookieSecurePolicy.None; // در محیط توسعه
//    options.ExpireTimeSpan = TimeSpan.FromDays(7);
//    options.SlidingExpiration = true;
//});

// افزودن سرویس‌های امنیتی برای بررسی و مدیریت امنیت
builder.Services.AddScoped<AuthenticationService>(); // سرویس احراز هویت سفارشی



var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();




app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
