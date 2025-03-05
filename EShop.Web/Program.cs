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
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie("UserAuth", options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.HttpOnly = true; // جلوگیری از دسترسی JavaScript به کوکی
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // فقط از طریق HTTPS ارسال شود
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
})
.AddCookie("AdminAuth", options =>
{
    options.LoginPath = "/Admin/Login";
    options.AccessDeniedPath = "/Admin/AccessDenied";
    options.Cookie.HttpOnly = true; // جلوگیری از دسترسی JavaScript به کوکی
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // فقط از طریق HTTPS ارسال شود
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});

#endregion


// ثبت سرویس‌ها
builder.Services.AddScoped<AuthenticationService>();
// به این:
builder.Services.AddScoped<JwtTokenManager>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new JwtTokenManager(
        configuration["Jwt:SecretKey"],
        configuration["Jwt:Issuer"],
        configuration["Jwt:Audience"]
    );
});
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
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
