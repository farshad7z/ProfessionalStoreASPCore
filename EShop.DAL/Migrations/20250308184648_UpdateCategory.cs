using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconColor",
                table: "productCategories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "IconColor",
                value: "#3b82f6");

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "IconColor",
                value: "#10b981");

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "IconColor",
                value: "#ec4899");

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "Description", "IconClass", "IconColor", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "کتاب‌های مختلف", "bi bi-book", "#f59e0b", 1, "کتاب‌ها", null, "books" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "Description", "IconClass", "IconColor", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "لوازم تحریر مدرسه و دفتر", "bi bi-pencil", "#ef4444", 1, "لوازم تحریر", null, "stationery" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 6,
                columns: new[] { "Description", "IconClass", "IconColor", "Name", "Slug" },
                values: new object[] { "موبایل‌های هوشمند", "bi bi-phone-fill", "#2563eb", "موبایل", "mobiles" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 7,
                columns: new[] { "Description", "IconClass", "IconColor", "Name", "Slug" },
                values: new object[] { "محصولات جانبی موبایل", "bi bi-headphones", "#6366f1", "لوازم جانبی موبایل", "mobile-accessories" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 8,
                columns: new[] { "Description", "IconClass", "IconColor", "Name", "ParentId", "Slug" },
                values: new object[] { "لپ‌تاپ‌های حرفه‌ای", "bi bi-laptop", "#3b82f6", "لپ‌تاپ", 1, "laptops" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 9,
                columns: new[] { "Description", "IconClass", "IconColor", "Name", "ParentId", "Slug" },
                values: new object[] { "تبلت‌های مختلف", "bi bi-tablet", "#10b981", "تبلت", 1, "tablets" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 10,
                columns: new[] { "Description", "IconClass", "IconColor", "Name", "ParentId", "Slug" },
                values: new object[] { "دوربین‌های عکاسی و فیلمبرداری", "bi bi-camera", "#6366f1", "دوربین عکاسی", 1, "cameras" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 11,
                columns: new[] { "Description", "IconClass", "IconColor", "Name", "ParentId", "Slug" },
                values: new object[] { "ابزارهای موردنیاز آشپزخانه", "bi bi-utensils", "#10b981", "ابزار آشپزخانه", 2, "kitchen-tools" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 12,
                columns: new[] { "Description", "IconClass", "IconColor", "Name", "ParentId", "Slug" },
                values: new object[] { "لوازم برقی آشپزخانه", "bi bi-plug", "#2563eb", "وسایل برقی آشپزخانه", 2, "kitchen-electronics" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 13,
                columns: new[] { "Description", "IconClass", "IconColor", "Name", "ParentId", "Slug" },
                values: new object[] { "مبلمان و دکوراسیون خانه", "bi bi-couch", "#6b4226", "مبلمان خانه", 2, "home-furniture" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 14,
                columns: new[] { "Description", "IconClass", "IconColor", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "لوازم خانگی برقی از قبیل یخچال، تلویزیون و...", "bi bi-tv", "#2563eb", 2, "لوازم خانگی برقی", 2, "home-appliances" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 15,
                columns: new[] { "Description", "IconClass", "IconColor", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "انواع لباس‌های مردانه", "bi bi-shirt", "#ec4899", 2, "لباس مردانه", 3, "mens-clothing" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 16,
                columns: new[] { "Description", "IconClass", "IconColor", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "انواع لباس‌های زنانه", "bi bi-dress", "#ec4899", 2, "لباس زنانه", 3, "womens-clothing" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 17,
                columns: new[] { "Description", "IconClass", "IconColor", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "کفش‌های مردانه و زنانه", "bi bi-shoe", "#ec4899", 2, "کفش", 3, "shoes" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 18,
                columns: new[] { "Description", "IconClass", "IconColor", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "اکسسوری‌ها و لوازم جانبی", "bi bi-hat", "#ec4899", 2, "اکسسوری", 3, "accessories" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 19,
                columns: new[] { "Description", "IconClass", "IconColor", "Name", "Slug" },
                values: new object[] { "محصولات زیبایی و بهداشتی", "bi bi-heart", "#a855f7", "زیبایی و سلامتی", "beauty-health" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 20,
                columns: new[] { "Description", "IconClass", "IconColor", "Name", "Slug" },
                values: new object[] { "موسیقی و فیلم‌های مختلف", "bi bi-music-note", "#ef4444", "موسیقی و فیلم", "music-films" });

            migrationBuilder.InsertData(
                table: "productCategories",
                columns: new[] { "CategoryId", "CreatedAt", "Description", "IconClass", "IconColor", "IsCategoryOnMain", "IsDeleted", "MenuType", "Name", "ParentId", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { 21, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "بازی‌های کامپیوتری و کنسول", "bi bi-controller", "#9ca3af", false, false, 0, "بازی‌های ویدئویی", null, "video-games", null },
                    { 22, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "مبلمان و دکوراسیون منزل", "bi bi-couch", "#6b4226", false, false, 0, "مبلمان", null, "furniture", null },
                    { 23, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "لوازم جانبی دیگر", "bi bi-plug", "#2563eb", false, false, 0, "لوازم جانبی", null, "accessories-other", null },
                    { 24, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "محصولات مربوط به خودرو", "bi bi-car-front", "#f97316", false, false, 1, "خودرو", null, "automobile", null },
                    { 25, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "لوازم موردنیاز سفر", "bi bi-suitcase", "#f97316", false, false, 1, "لوازم سفر", null, "travel", null },
                    { 26, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "محصولات تکنولوژی پیشرفته", "bi bi-gear", "#3b82f6", false, false, 1, "تکنولوژی", null, "technology", null },
                    { 27, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "محصولات مربوط به حیوانات خانگی", "bi bi-paw", "#ec4899", false, false, 1, "حیوانات خانگی", null, "pets", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 27);

            migrationBuilder.DropColumn(
                name: "IconColor",
                table: "productCategories");

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "Description", "IconClass", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "موبایل‌های هوشمند", "bi bi-phone-fill", 2, "موبایل", 1, "mobiles" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "Description", "IconClass", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "محصولات جانبی موبایل", "bi bi-headphones", 2, "لوازم جانبی موبایل", 1, "mobile-accessories" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 6,
                columns: new[] { "Description", "IconClass", "Name", "Slug" },
                values: new object[] { "لپ‌تاپ‌های حرفه‌ای", "bi bi-laptop", "لپ‌تاپ", "laptops" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 7,
                columns: new[] { "Description", "IconClass", "Name", "Slug" },
                values: new object[] { "انواع هدفون و هندزفری", "bi bi-earbuds", "هدفون و هندزفری", "headphones-earbuds" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 8,
                columns: new[] { "Description", "IconClass", "Name", "ParentId", "Slug" },
                values: new object[] { "ابزارهای موردنیاز آشپزخانه", "bi bi-utensils", "ابزار آشپزخانه", 2, "kitchen-tools" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 9,
                columns: new[] { "Description", "IconClass", "Name", "ParentId", "Slug" },
                values: new object[] { "لوازم برقی آشپزخانه", "bi bi-plug", "وسایل برقی آشپزخانه", 2, "kitchen-electronics" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 10,
                columns: new[] { "Description", "IconClass", "Name", "ParentId", "Slug" },
                values: new object[] { "انواع فرش و موکت", "bi bi-carpet", "فرش و موکت", 2, "carpets-rugs" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 11,
                columns: new[] { "Description", "IconClass", "Name", "ParentId", "Slug" },
                values: new object[] { "انواع لباس‌های مردانه", "bi bi-shirt", "لباس مردانه", 3, "mens-clothing" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 12,
                columns: new[] { "Description", "IconClass", "Name", "ParentId", "Slug" },
                values: new object[] { "انواع لباس‌های زنانه", "bi bi-dress", "لباس زنانه", 3, "womens-clothing" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 13,
                columns: new[] { "Description", "IconClass", "Name", "ParentId", "Slug" },
                values: new object[] { "انواع کفش‌های مردانه و زنانه", "bi bi-shoe", "کفش", 3, "shoes" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 14,
                columns: new[] { "Description", "IconClass", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "کتاب‌های مختلف", "bi bi-book", 0, "کتاب‌ها", null, "books" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 15,
                columns: new[] { "Description", "IconClass", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "لوازم تحریر مدرسه و دفتر", "bi bi-pencil", 0, "لوازم تحریر", null, "stationery" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 16,
                columns: new[] { "Description", "IconClass", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "لوازم ورزشی و مسافرتی", "bi bi-bicycle", 0, "ورزش و سفر", null, "sports-travel" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 17,
                columns: new[] { "Description", "IconClass", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "محصولات زیبایی و بهداشتی", "bi bi-heart", 0, "زیبایی و سلامتی", null, "beauty-health" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 18,
                columns: new[] { "Description", "IconClass", "MenuType", "Name", "ParentId", "Slug" },
                values: new object[] { "موسیقی و فیلم‌های مختلف", "bi bi-music-note", 0, "موسیقی و فیلم", null, "music-films" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 19,
                columns: new[] { "Description", "IconClass", "Name", "Slug" },
                values: new object[] { "بازی‌های کامپیوتری و کنسول", "bi bi-controller", "بازی‌های ویدئویی", "video-games" });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 20,
                columns: new[] { "Description", "IconClass", "Name", "Slug" },
                values: new object[] { "مبلمان و دکوراسیون منزل", "bi bi-couch", "مبلمان", "furniture" });
        }
    }
}
