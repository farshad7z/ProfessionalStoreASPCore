using EShop.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.Constants;


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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Table attribute definition
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

            #region Relations
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

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.HasOne(uc => uc.User)
                      .WithMany(u => u.UserClaims)
                      .HasForeignKey(uc => uc.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_UserClaims_Users");

                entity.HasOne(uc => uc.Claim)
                      .WithMany(r => r.UserClaims)
                      .HasForeignKey(ur => ur.ClaimId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_UserRole_Claims");
            });

            #endregion

            #region Seed
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

        // کلایم‌های مدیریتی برای سایت
        new AppClaim { ClaimId = 9, ClaimType = ConstClaims.AdminCanManageShops, Value = "مدیر سایت - مدیریت فروشگاه‌ها" },
        new AppClaim { ClaimId = 10, ClaimType = ConstClaims.AdminCanEditShops, Value = "مدیر سایت - ویرایش فروشگاه‌ها" },
        new AppClaim { ClaimId = 11, ClaimType = ConstClaims.AdminCanRemoveShops, Value = "مدیر سایت - حذف فروشگاه‌ها" },
        new AppClaim { ClaimId = 12, ClaimType = ConstClaims.AdminCanEditProducts, Value = "مدیر سایت - ویرایش محصولات" },
        new AppClaim { ClaimId = 13, ClaimType = ConstClaims.AdminCanRemoveProducts, Value = "مدیر سایت - حذف محصولات" },
        new AppClaim { ClaimId = 14, ClaimType = ConstClaims.AdminCanManageOrders, Value = "مدیر سایت - مدیریت سفارشات" },
        new AppClaim { ClaimId = 15, ClaimType = ConstClaims.AdminCanAddUsers, Value = "مدیر سایت - افزودن کاربران" },
        new AppClaim { ClaimId = 16, ClaimType = ConstClaims.AdminCanEditUsers, Value = "مدیر سایت - ویرایش کاربران" },
        new AppClaim { ClaimId = 17, ClaimType = ConstClaims.AdminCanRemoveUsers, Value = "مدیر سایت - حذف کاربران" },
        new AppClaim { ClaimId = 18, ClaimType = ConstClaims.AdminCanReplayComments, Value = "مدیر سایت - پاسخ به نظرات" },
        new AppClaim { ClaimId = 19, ClaimType = ConstClaims.AdminManageAccessUsers, Value = "مدیر سایت - مدیریت دسترسی کاربران" }
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "فرشاد",
                    LastName = "زمانی",
                    PhoneNumber = "09109999414",
                    HasShop = true,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0), // ✅ مقدار ثابت
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