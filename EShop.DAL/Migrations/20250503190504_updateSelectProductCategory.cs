using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateSelectProductCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMainCategory",
                table: "ProductSelectCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainCategory",
                table: "ProductSelectCategories");
        }
    }
}
