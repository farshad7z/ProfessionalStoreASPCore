using EShop.DAL.Context;
using Microsoft.EntityFrameworkCore;
using EShop.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);



#region
builder.Services.AddDbContext<EShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

// خواندن ConnectionString از appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// اضافه کردن سرویس‌های پروژه
builder.Services.AddApplicationServices();

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
