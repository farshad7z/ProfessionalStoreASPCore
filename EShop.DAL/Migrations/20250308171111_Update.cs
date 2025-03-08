using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCategoryOnMain",
                table: "productCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "productCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 6,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 7,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 8,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 9,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 10,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 11,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 12,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 13,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 14,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 15,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 16,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 17,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 18,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 19,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "productCategories",
                keyColumn: "CategoryId",
                keyValue: 20,
                columns: new[] { "IsCategoryOnMain", "IsDeleted" },
                values: new object[] { false, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCategoryOnMain",
                table: "productCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "productCategories");
        }
    }
}
