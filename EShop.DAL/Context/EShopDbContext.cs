using EShop.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.Constants;
using EShop.Core.Enums;
using EShop.Core.Entities.Models.Products;


namespace EShop.DAL.Context
{
    public class EShopDbContext : DbContext
    {
        public EShopDbContext() { }
        public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options) { }

        #region User
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<AppClaim> AppClaims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        #endregion

        #region Product

        public DbSet<ProductFeatureValue> ProductFeatureValues { get; set; }
        public DbSet<CategoryFeature> CategoryFeatures { get; set; }
        public DbSet<ProductGallery> ProductGalleries { set; get; }
        public DbSet<ProductSEO> ProductSEOs { get; set; }
        public DbSet<ProductSelectCategory> ProductSelectCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        #endregion

        #region Shop

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Employee> Employees { get; set; }

        #endregion

        #region Public
        public DbSet<Slide> Slides { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Table attribute definition

            #region Product
            modelBuilder.Entity<ProductFeatureValue>(entity =>
            entity.HasKey(pfv => pfv.Id));

            modelBuilder.Entity<CategoryFeature>(entity =>
           entity.HasKey(cf => cf.Id));

            modelBuilder.Entity<ProductGallery>(entity =>
                       entity.HasKey(pg => pg.GalleryId));

            modelBuilder.Entity<ProductSEO>(entity =>
            entity.HasKey(pc => pc.Id));

            modelBuilder.Entity<ProductSelectCategory>(entity =>
            entity.HasKey(pc => pc.Id));

            modelBuilder.Entity<Product>(entity =>
            entity.HasKey(p => p.Id));

            modelBuilder.Entity<ProductCategory>(entity =>
            entity.HasKey(pc => pc.CategoryId));

            #endregion

            #region Public
            modelBuilder.Entity<Shop>(entity =>
                    {
                        entity.HasKey(s => s.ShopId);
                    });

            modelBuilder.Entity<Slide>(entity =>
            entity.HasKey(s => s.SlideId));



            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.RoleId);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId })
                      .HasName("PK_UserRoles");
            });

            modelBuilder.Entity<AppClaim>(entity =>
            {
                entity.HasKey(c => c.ClaimId);
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.HasKey(uc => new { uc.UserId, uc.ClaimId })
                      .HasName("PK_UserClaims");
            });
            #endregion


            #endregion

            #region Relations

            #region Product

            modelBuilder.Entity<ProductFeatureValue>(entity =>
            {
                entity.HasOne(pf => pf.Feature) // ارتباط یک به یک با CategoryFeature
                      .WithMany() // هر ویژگی ممکن است چندین ویژگی مقدار داشته باشد
                      .HasForeignKey(pf => pf.FeatureId) // تنظیم کلید خارجی
                      .OnDelete(DeleteBehavior.Cascade); // حذف رفتار مناسب در صورت حذف
            });

            modelBuilder.Entity<CategoryFeature>(entity =>
            {
                entity.HasOne(cf => cf.Category)
                  .WithMany(c => c.Features)
                  .HasForeignKey(cf => cf.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<ProductGallery>(entity =>
                   entity.HasOne(pg => pg.Product)
                  .WithMany(p => p.Galleries)
                  .HasForeignKey(pg => pg.ProductId)
                  .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<ProductSEO>(entity =>
            entity.HasOne(ps => ps.Product)
            .WithOne(p => p.ProductSEO)
            .OnDelete(DeleteBehavior.Cascade)); // حذف شدن SEO هنگام حذف محصول

            modelBuilder.Entity<ProductSelectCategory>(entity =>
            {

                entity.HasOne(psc => psc.Product)
                .WithMany(p => p.ProductSelectCategory)
                .HasForeignKey(psc => psc.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("FK_ProductSelectCategory_Product");

                entity.HasOne(psc => psc.ProductCategory)
                .WithMany(pc => pc.ProductSelectCategory)
                .HasForeignKey(psc => psc.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict) // جلوگیری از حذف دسته‌ای که هنوز محصولی دارد
                .HasConstraintName("FK_ProductSelectCategory_ProductCategory");
            });

            modelBuilder.Entity<Product>(entity =>
            entity.HasOne(p => p.Shop)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.ShopId)
             .OnDelete(DeleteBehavior.Restrict));

            modelBuilder.Entity<ProductCategory>(entity =>
            entity.HasOne(pc => pc.Parent)
            .WithMany(p => p.Children)
            .HasForeignKey(PC => PC.ParentId)
            .OnDelete(DeleteBehavior.Restrict)); // جلوگیری از حذف دسته‌بندی‌ها اگر والد حذف شود

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(ur => ur.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_UserRoles_Users");

                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_UserRole_Roles");
            });
            #endregion

            #region Public
            modelBuilder.Entity<Employee>(entity =>
                    {
                        entity.HasOne(e => e.Shop)
                              .WithMany(s => s.Employees)
                              .HasForeignKey(e => e.ShopId)
                              .OnDelete(DeleteBehavior.Cascade); // حذف کارمندان در صورت حذف فروشگاه
                    });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasKey(s => s.ShopId);

                entity.HasOne(s => s.User)
                      .WithOne(u => u.OwnedShop)
                      .HasForeignKey<Shop>(s => s.OwnerId)
                      .OnDelete(DeleteBehavior.Restrict); // Cascade delete, delete the shop when the user is deleted
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.HasOne(uc => uc.User)
                      .WithMany(u => u.UserClaims)
                      .HasForeignKey(uc => uc.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_UserClaims_Users");

                entity.HasOne(uc => uc.Claim)
                        .WithMany(c => c.UserClaims)
                        .HasForeignKey(uc => uc.ClaimId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_UserClaims_Claims");
            });
            #endregion






            #endregion

            #region Seed

            #region Start Shop

            modelBuilder.Entity<UserClaim>().HasData(
                new UserClaim
                {
                    UserId = 1,
                    ClaimId = 20, // دسترسی CanManageShop
                    ClaimValue = ConstClaims.CanManageShop
                },
                new UserClaim
                {
                    UserId = 1,
                    ClaimId = 21, // دسترسی CanEditProducts
                    ClaimValue = ConstClaims.CanEditProducts
                });

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    UserId = 1, // مرتبط با کاربر بالا
                    ShopId = 1, // مرتبط با فروشگاه بازارپال
                    Address = "زنجان، ابهر، خیابان اصلی، پلاک ۱۲",
                    SecondaryPhoneNumber = "09058794262",
                    NationalCode = "4400230147",
                    BankAccountIBAN = "IR021000001330000544121545", // اضافه کردن پیشوند IR
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 3, 6),
                });

            #endregion

            modelBuilder.Entity<Shop>().HasData(
                new Shop
                {
                    ShopId = 1,
                    ShopNameFa = "بازارپال",
                    ShopNameEn = "BazarPal",
                    Description = "فروشگاه اینترنتی مدرن با تنوع بالا در محصولات الکترونیکی، پوشاک و لوازم خانگی. ارائه دهنده بهترین قیمت‌ها با تضمین کیفیت!",
                    FullAddress = "زنجان، ابهر، خیابان اصلی، پلاک 21",
                    PostalCode = "445452654",
                    PhoneNumber = "09109999414",
                    Email = "BazarPal_info@gmail.com",
                    Latitude = 36.1468m,
                    Longitude = 49.2332m,
                    RegistrationDate = new DateTime(2025, 3, 6),
                    IsActive = true,
                    OwnerId = 1 // فرض می‌کنیم کاربر با ID=1 مالک است
                }
);


            modelBuilder.Entity<ProductCategory>().HasData(
                new List<ProductCategory>
                {
        // 🟢 دسته‌بندی‌های منوی اصلی
        new ProductCategory
        {
            CategoryId = 1,
            Name = "الکترونیک",
            Description = "محصولات الکترونیکی",
            MenuType = MenuType.MainMenu,
            IconClass = "bi bi-phone",
            Slug = "electronics",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#3b82f6" // آبی
        },
        new ProductCategory
        {
            CategoryId = 2,
            Name = "خانه و آشپزخانه",
            Description = "ابزارهای خانگی",
            MenuType = MenuType.MainMenu,
            IconClass = "bi bi-house-door",
            Slug = "home-kitchen",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#10b981" // سبز
        },
        new ProductCategory
        {
            CategoryId = 3,
            Name = "مد و پوشاک",
            Description = "لباس و پوشاک",
            MenuType = MenuType.MainMenu,
            IconClass = "bi bi-bag",
            Slug = "fashion-clothing",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#ec4899" // صورتی
        },
        new ProductCategory
        {
            CategoryId = 4,
            Name = "کتاب‌ها",
            Description = "کتاب‌های مختلف",
            MenuType = MenuType.MainMenu,
            IconClass = "bi bi-book",
            Slug = "books",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#f59e0b" // زرد
        },
        new ProductCategory
        {
            CategoryId = 5,
            Name = "لوازم تحریر",
            Description = "لوازم تحریر مدرسه و دفتر",
            MenuType = MenuType.MainMenu,
            IconClass = "bi bi-pencil",
            Slug = "stationery",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#ef4444" // قرمز
        },

        // 🟡 زیردسته‌های الکترونیک
        new ProductCategory
        {
            CategoryId = 6,
            Name = "موبایل",
            Description = "موبایل‌های هوشمند",
            MenuType = MenuType.SecondaryMenu,
            ParentId = 1,
            IconClass = "bi bi-phone-fill",
            Slug = "mobiles",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#2563eb" // آبی روشن
        },
        new ProductCategory
        {
            CategoryId = 7,
            Name = "لوازم جانبی موبایل",
            Description = "محصولات جانبی موبایل",
            MenuType = MenuType.SecondaryMenu,
            ParentId = 1,
            IconClass = "bi bi-headphones",
            Slug = "mobile-accessories",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#6366f1" // بنفش روشن
        },
        new ProductCategory
        {
            CategoryId = 8,
            Name = "لپ‌تاپ",
            Description = "لپ‌تاپ‌های حرفه‌ای",
            MenuType = MenuType.SecondaryMenu,
            ParentId = 1,
            IconClass = "bi bi-laptop",
            Slug = "laptops",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#3b82f6" // آبی
        },
        new ProductCategory
        {
            CategoryId = 9,
            Name = "تبلت",
            Description = "تبلت‌های مختلف",
            MenuType = MenuType.SecondaryMenu,
            ParentId = 1,
            IconClass = "bi bi-tablet",
            Slug = "tablets",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#10b981" // سبز
        },
        new ProductCategory
        {
            CategoryId = 10,
            Name = "دوربین عکاسی",
            Description = "دوربین‌های عکاسی و فیلمبرداری",
            MenuType = MenuType.SecondaryMenu,
            ParentId = 1,
            IconClass = "bi bi-camera",
            Slug = "cameras",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#6366f1" // بنفش روشن
        },

        // 🟡 زیردسته‌های خانه و آشپزخانه
        new ProductCategory
        {
            CategoryId = 11,
            Name = "ابزار آشپزخانه",
            Description = "ابزارهای موردنیاز آشپزخانه",
            MenuType = MenuType.SecondaryMenu,
            ParentId = 2,
            IconClass = "bi bi-utensils",
            Slug = "kitchen-tools",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#10b981" // سبز
        },
        new ProductCategory
        {
            CategoryId = 12,
            Name = "وسایل برقی آشپزخانه",
            Description = "لوازم برقی آشپزخانه",
            MenuType = MenuType.SecondaryMenu,
            ParentId = 2,
            IconClass = "bi bi-plug",
            Slug = "kitchen-electronics",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#2563eb" // آبی روشن
        },
        new ProductCategory
        {
            CategoryId = 13,
            Name = "مبلمان خانه",
            Description = "مبلمان و دکوراسیون خانه",
            MenuType = MenuType.SecondaryMenu,
            ParentId = 2,
            IconClass = "bi bi-couch",
            Slug = "home-furniture",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#6b4226" // قهوه‌ای
        },
        new ProductCategory
        {
            CategoryId = 14,
            Name = "لوازم خانگی برقی",
            Description = "لوازم خانگی برقی از قبیل یخچال، تلویزیون و...",
            MenuType = MenuType.SecondaryMenu,
            ParentId = 2,
            IconClass = "bi bi-tv",
            Slug = "home-appliances",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#2563eb" // آبی روشن
        },

        // 🟡 زیردسته‌های مد و پوشاک
        new ProductCategory
        {
            CategoryId = 15,
            Name = "لباس مردانه",
            Description = "انواع لباس‌های مردانه",
            MenuType = MenuType.SecondaryMenu,
            ParentId = 3,
            IconClass = "bi bi-shirt",
            Slug = "mens-clothing",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#ec4899" // صورتی
        },
        new ProductCategory
        {
            CategoryId = 16,
            Name = "لباس زنانه",
            Description = "انواع لباس‌های زنانه",
            MenuType = MenuType.SecondaryMenu,
            ParentId = 3,
            IconClass = "bi bi-dress",
            Slug = "womens-clothing",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#ec4899" // صورتی
        },
        new ProductCategory
        {
            CategoryId = 17,
            Name = "کفش",
            Description = "کفش‌های مردانه و زنانه",
            MenuType = MenuType.SecondaryMenu,
            ParentId = 3,
            IconClass = "bi bi-shoe",
            Slug = "shoes",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#ec4899" // صورتی
        },
        new ProductCategory
        {
            CategoryId = 18,
            Name = "اکسسوری",
            Description = "اکسسوری‌ها و لوازم جانبی",
            MenuType = MenuType.SecondaryMenu,
            ParentId = 3,
            IconClass = "bi bi-hat",
            Slug = "accessories",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#ec4899" // صورتی
        },

        // 🔵 دسته‌بندی‌های عمومی
        new ProductCategory
        {
            CategoryId = 19,
            Name = "زیبایی و سلامتی",
            Description = "محصولات زیبایی و بهداشتی",
            MenuType = MenuType.CategoryOnly,
            IconClass = "bi bi-heart",
            Slug = "beauty-health",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#a855f7" // بنفش
        },
        new ProductCategory
        {
            CategoryId = 20,
            Name = "موسیقی و فیلم",
            Description = "موسیقی و فیلم‌های مختلف",
            MenuType = MenuType.CategoryOnly,
            IconClass = "bi bi-music-note",
            Slug = "music-films",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#ef4444" // قرمز
        },
        new ProductCategory
        {
            CategoryId = 21,
            Name = "بازی‌های ویدئویی",
            Description = "بازی‌های کامپیوتری و کنسول",
            MenuType = MenuType.CategoryOnly,
            IconClass = "bi bi-controller",
            Slug = "video-games",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#9ca3af" // خاکی
        },
        new ProductCategory
        {
            CategoryId = 22,
            Name = "مبلمان",
            Description = "مبلمان و دکوراسیون منزل",
            MenuType = MenuType.CategoryOnly,
            IconClass = "bi bi-couch",
            Slug = "furniture",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#6b4226" // قهوه‌ای
        },
        new ProductCategory
        {
            CategoryId = 23,
            Name = "لوازم جانبی",
            Description = "لوازم جانبی دیگر",
            MenuType = MenuType.CategoryOnly,
            IconClass = "bi bi-plug",
            Slug = "accessories-other",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#2563eb" // آبی روشن
        },

        // 🟠 سایر دسته‌ها
        new ProductCategory
        {
            CategoryId = 24,
            Name = "خودرو",
            Description = "محصولات مربوط به خودرو",
            MenuType = MenuType.MainMenu,
            IconClass = "bi bi-car-front",
            Slug = "automobile",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#f97316" // نارنجی
        },
        new ProductCategory
        {
            CategoryId = 25,
            Name = "لوازم سفر",
            Description = "لوازم موردنیاز سفر",
            MenuType = MenuType.MainMenu,
            IconClass = "bi bi-suitcase",
            Slug = "travel",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#f97316" // نارنجی
        },
        new ProductCategory
        {
            CategoryId = 26,
            Name = "تکنولوژی",
            Description = "محصولات تکنولوژی پیشرفته",
            MenuType = MenuType.MainMenu,
            IconClass = "bi bi-gear",
            Slug = "technology",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#3b82f6" // آبی
        },
        new ProductCategory
        {
            CategoryId = 27,
            Name = "حیوانات خانگی",
            Description = "محصولات مربوط به حیوانات خانگی",
            MenuType = MenuType.MainMenu,
            IconClass = "bi bi-paw",
            Slug = "pets",
            CreatedAt = new DateTime(2025, 3, 6),
            IconColor = "#ec4899" // صورتی
        }
                }
            );



            modelBuilder.Entity<Role>().HasData(
                new List<Role>
                {
                    new Role { RoleId = 1, RoleName = "SuperAdmin", RoleTitle = "مدیر اصلی سایت" },
                    new Role { RoleId = 2, RoleName = "Admin", RoleTitle = "مدیر سایت" },
                    new Role { RoleId = 3, RoleName = "ShopOwner", RoleTitle = "صاحب فروشگاه" },
                    new Role { RoleId = 4, RoleName = "ShopManager", RoleTitle = "مدیر فروشگاه" },
                    new Role { RoleId = 5, RoleName = "Customer", RoleTitle = "کاربر عادی" },
                    new Role { RoleId = 6, RoleName = "DeliveryAgent", RoleTitle = "مسئول ارسال" }
                });

            modelBuilder.Entity<AppClaim>().HasData(
                new List<AppClaim>
                {
                    // کلایم‌های مربوط به مدیریت کاربران سایت
                    new AppClaim { ClaimId = 1, ClaimType = ConstClaims.CanManageUsers, Value = "مدیریت کارمندان فروشگاه" },
                    new AppClaim { ClaimId = 2, ClaimType = ConstClaims.CanAddUsersOnShop, Value = "اضافه کردن کارمند به فروشگاه" },
                    new AppClaim { ClaimId = 3, ClaimType = ConstClaims.CanRemoveUsersOnShop, Value = "حذف کارمند از فروشگاه" },
                    
                    // کلایم‌های مربوط به فروشگاه
                    new AppClaim { ClaimId = 4, ClaimType = ConstClaims.CanManageShop, Value = "دسترسی به مدیریت فروشگاه" },
                    new AppClaim { ClaimId = 5, ClaimType = ConstClaims.CanEditProducts, Value = "دسترسی به ویرایش محصولات" },
                    new AppClaim { ClaimId = 6, ClaimType = ConstClaims.CanRemoveProducts, Value = "دسترسی به حذف محصولات" },
                    new AppClaim { ClaimId = 7, ClaimType = ConstClaims.CanManageOrders, Value = "دسترسی به مدیریت سفارش‌ها" },
                    new AppClaim { ClaimId = 8, ClaimType = ConstClaims.CanManageContent, Value = "دسترسی به مدیریت محتوا" },
                    new AppClaim { ClaimId = 9, ClaimType =ConstClaims.CanAddProducts, Value = "افزودن محصولات فروشگاه" },
                    new AppClaim { ClaimId = 10, ClaimType =ConstClaims.ManageAccessUsers, Value = "مدیریت دسترسی کاربران فروشگاه" },

                    
                    // کلایم‌های مدیریتی برای سایت
     
                    new AppClaim { ClaimId = 11, ClaimType = ConstClaims.AdminCanManageShops, Value = "مدیر سایت - مدیریت فروشگاه‌ها" },
                    new AppClaim { ClaimId = 12, ClaimType = ConstClaims.AdminCanEditShops, Value = "مدیر سایت - ویرایش فروشگاه‌ها" },
                    new AppClaim { ClaimId = 13, ClaimType = ConstClaims.AdminCanRemoveShops, Value = "مدیر سایت - حذف فروشگاه‌ها" },
                    new AppClaim { ClaimId = 14, ClaimType = ConstClaims.AdminCanEditProducts, Value = "مدیر سایت - ویرایش محصولات" },
                    new AppClaim { ClaimId = 15, ClaimType = ConstClaims.AdminCanRemoveProducts, Value = "مدیر سایت - حذف محصولات" },
                    new AppClaim { ClaimId = 16, ClaimType = ConstClaims.AdminCanManageOrders, Value = "مدیر سایت - مدیریت سفارشات" },
                    new AppClaim { ClaimId = 17, ClaimType = ConstClaims.AdminCanAddUsers, Value = "مدیر سایت - افزودن کاربران" },
                    new AppClaim { ClaimId = 18, ClaimType = ConstClaims.AdminCanEditUsers, Value = "مدیر سایت - ویرایش کاربران" },
                    new AppClaim { ClaimId = 19, ClaimType = ConstClaims.AdminCanRemoveUsers, Value = "مدیر سایت - حذف کاربران" },
                    new AppClaim { ClaimId = 20, ClaimType = ConstClaims.AdminCanReplayComments, Value = "مدیر سایت - پاسخ به نظرات" },
                    new AppClaim { ClaimId = 21, ClaimType = ConstClaims.AdminManageAccessUsers, Value = "مدیر سایت - مدیریت دسترسی کاربران" }
                });


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "فرشاد",
                    LastName = "زمانی",
                    PhoneNumber = "09109999414",
                    IsEmployeeShop = true,
                    IsEmployeeSite = true,
                    IsActive = true,
                    RegistrationDate = new DateTime(2025, 1, 1, 12, 0, 0), // ✅ مقدار ثابت
                    //PasswordHash = PasswordHasher.HashPassword("123456")
                });

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    UserId = 1, // ID کاربر
                    RoleId = 2  // ID نقش SuperAdmin
                });



            #endregion


        }
    }
}