using EShop.DAL.Context;
using Microsoft.EntityFrameworkCore;
using EShop.Infrastructure.DependencyInjection;
using EShop.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.Cookies;

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


// تنظیمات احراز هویت (Authentication)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {        //options.LoginPath = "/Account/Login"; // مسیر ورود
        //options.LogoutPath = "/Account/Logout"; // مسیر خروج
        //options.ExpireTimeSpan = TimeSpan.FromDays(7); // مدت زمان اعتبار کوکی
        //options.SlidingExpiration = true; // تمدید اعتبار کوکی

    });
// اضافه کردن سرویس AuthenticationService
builder.Services.AddScoped<AuthenticationService>();
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
