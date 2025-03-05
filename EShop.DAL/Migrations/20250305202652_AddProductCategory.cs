using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProductCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Claims",
                table: "UserClaims");

            migrationBuilder.CreateTable(
                name: "productCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    MenuType = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productCategories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_productCategories_productCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "productCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productCategories_ParentId",
                table: "productCategories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_Claims",
                table: "UserClaims",
                column: "ClaimId",
                principalTable: "AppClaims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_Claims",
                table: "UserClaims");

            migrationBuilder.DropTable(
                name: "productCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Claims",
                table: "UserClaims",
                column: "ClaimId",
                principalTable: "AppClaims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
