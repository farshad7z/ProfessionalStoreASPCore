using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedProductCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "productCategories",
                columns: new[] { "CategoryId", "CreatedAt", "Description", "IconClass", "MenuType", "Name", "ParentId", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "محصولات الکترونیکی", "bi bi-phone", 1, "الکترونیک", null, "electronics", null },
                    { 2, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "ابزارهای خانگی", "bi bi-house-door", 1, "خانه و آشپزخانه", null, "home-kitchen", null },
                    { 3, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "لباس و پوشاک", "bi bi-bag", 1, "مد و پوشاک", null, "fashion-clothing", null },
                    { 14, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "کتاب‌های مختلف", "bi bi-book", 0, "کتاب‌ها", null, "books", null },
                    { 15, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "لوازم تحریر مدرسه و دفتر", "bi bi-pencil", 0, "لوازم تحریر", null, "stationery", null },
                    { 16, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "لوازم ورزشی و مسافرتی", "bi bi-bicycle", 0, "ورزش و سفر", null, "sports-travel", null },
                    { 17, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "محصولات زیبایی و بهداشتی", "bi bi-heart", 0, "زیبایی و سلامتی", null, "beauty-health", null },
                    { 18, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "موسیقی و فیلم‌های مختلف", "bi bi-music-note", 0, "موسیقی و فیلم", null, "music-films", null },
                    { 19, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "بازی‌های کامپیوتری و کنسول", "bi bi-controller", 0, "بازی‌های ویدئویی", null, "video-games", null },
                    { 20, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "مبلمان و دکوراسیون منزل", "bi bi-couch", 0, "مبلمان", null, "furniture", null },
                    { 4, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "موبایل‌های هوشمند", "bi bi-phone-fill", 2, "موبایل", 1, "mobiles", null },
                    { 5, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "محصولات جانبی موبایل", "bi bi-headphones", 2, "لوازم جانبی موبایل", 1, "mobile-accessories", null },
                    { 6, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "لپ‌تاپ‌های حرفه‌ای", "bi bi-laptop", 2, "لپ‌تاپ", 1, "laptops", null },
                    { 7, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "انواع هدفون و هندزفری", "bi bi-earbuds", 2, "هدفون و هندزفری", 1, "headphones-earbuds", null },
                    { 8, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "ابزارهای موردنیاز آشپزخانه", "bi bi-utensils", 2, "ابزار آشپزخانه", 2, "kitchen-tools", null },
                    { 9, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "لوازم برقی آشپزخانه", "bi bi-plug", 2, "وسایل برقی آشپزخانه", 2, "kitchen-electronics", null },
                    { 10, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "انواع فرش و موکت", "bi bi-carpet", 2, "فرش و موکت", 2, "carpets-rugs", null },
                    { 11, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "انواع لباس‌های مردانه", "bi bi-shirt", 2, "لباس مردانه", 3, "mens-clothing", null },
                    { 12, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "انواع لباس‌های زنانه", "bi bi-dress", 2, "لباس زنانه", 3, "womens-clothing", null },
                    { 13, new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "انواع کفش‌های مردانه و زنانه", "bi bi-shoe", 2, "کفش", 3, "shoes", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 3);
        }
    }
}
