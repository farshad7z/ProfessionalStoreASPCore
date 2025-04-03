using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateFeatur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatureValues_CategoryFeatures_CategoryFeatureId",
                table: "ProductFeatureValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatureValues_CategoryFeatures_FeatureId",
                table: "ProductFeatureValues");

            migrationBuilder.DropTable(
                name: "CategoryFeatures");

            migrationBuilder.RenameColumn(
                name: "CategoryFeatureId",
                table: "ProductFeatureValues",
                newName: "ProductId1");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFeatureValues_CategoryFeatureId",
                table: "ProductFeatureValues",
                newName: "IX_ProductFeatureValues_ProductId1");

            migrationBuilder.AlterColumn<int>(
                name: "FeatureId",
                table: "ProductFeatureValues",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductFeatureValues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryFeatureValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFeatureValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryFeatureValues_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryFeatureValues_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatureValues_ProductId",
                table: "ProductFeatureValues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFeatureValues_CategoryId",
                table: "CategoryFeatureValues",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFeatureValues_FeatureId",
                table: "CategoryFeatureValues",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatureValues_Features_FeatureId",
                table: "ProductFeatureValues",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatureValues_Products_ProductId",
                table: "ProductFeatureValues",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatureValues_Products_ProductId1",
                table: "ProductFeatureValues",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatureValues_Features_FeatureId",
                table: "ProductFeatureValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatureValues_Products_ProductId",
                table: "ProductFeatureValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatureValues_Products_ProductId1",
                table: "ProductFeatureValues");

            migrationBuilder.DropTable(
                name: "CategoryFeatureValues");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatureValues_ProductId",
                table: "ProductFeatureValues");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductFeatureValues");

            migrationBuilder.RenameColumn(
                name: "ProductId1",
                table: "ProductFeatureValues",
                newName: "CategoryFeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFeatureValues_ProductId1",
                table: "ProductFeatureValues",
                newName: "IX_ProductFeatureValues_CategoryFeatureId");

            migrationBuilder.AlterColumn<int>(
                name: "FeatureId",
                table: "ProductFeatureValues",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryFeatures_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFeatures_CategoryId",
                table: "CategoryFeatures",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatureValues_CategoryFeatures_CategoryFeatureId",
                table: "ProductFeatureValues",
                column: "CategoryFeatureId",
                principalTable: "CategoryFeatures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatureValues_CategoryFeatures_FeatureId",
                table: "ProductFeatureValues",
                column: "FeatureId",
                principalTable: "CategoryFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
