using EShop.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


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

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "فرشاد",
                    LastName = "زمانی",
                    PhoneNumber = "09109999414",
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