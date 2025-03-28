using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class features : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productCategories_productCategories_ParentId",
                table: "productCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatureValue_ProductFeature_ProductFeatureId",
                table: "ProductFeatureValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantFeature_ProductFeatureValue_ProductFeatureValueId",
                table: "ProductVariantFeature");

            migrationBuilder.DropTable(
                name: "ProductFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productCategories",
                table: "productCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductFeatureValue",
                table: "ProductFeatureValue");

            migrationBuilder.RenameTable(
                name: "productCategories",
                newName: "ProductCategories");

            migrationBuilder.RenameTable(
                name: "ProductFeatureValue",
                newName: "ProductFeatureValues");

            migrationBuilder.RenameIndex(
                name: "IX_productCategories_ParentId",
                table: "ProductCategories",
                newName: "IX_ProductCategories_ParentId");

            migrationBuilder.RenameColumn(
                name: "ProductFeatureId",
                table: "ProductFeatureValues",
                newName: "FeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFeatureValue_ProductFeatureId",
                table: "ProductFeatureValues",
                newName: "IX_ProductFeatureValues_FeatureId");

            migrationBuilder.AlterColumn<string>(
                name: "MetaKeywords",
                table: "ProductSEOs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "ProductCategories",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryFeatureId",
                table: "ProductFeatureValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories",
                column: "CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductFeatureValues",
                table: "ProductFeatureValues",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CategoryFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 6,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 7,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 8,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 9,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 10,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 11,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 12,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 13,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 14,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 15,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 16,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 17,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 18,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 19,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 20,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 21,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 22,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 23,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 24,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 25,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 26,
                column: "ImageName",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "CategoryId",
                keyValue: 27,
                column: "ImageName",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatureValues_CategoryFeatureId",
                table: "ProductFeatureValues",
                column: "CategoryFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFeatures_CategoryId",
                table: "CategoryFeatures",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_ProductCategories_ParentId",
                table: "ProductCategories",
                column: "ParentId",
                principalTable: "ProductCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantFeature_ProductFeatureValues_ProductFeatureValueId",
                table: "ProductVariantFeature",
                column: "ProductFeatureValueId",
                principalTable: "ProductFeatureValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_ProductCategories_ParentId",
                table: "ProductCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatureValues_CategoryFeatures_CategoryFeatureId",
                table: "ProductFeatureValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatureValues_CategoryFeatures_FeatureId",
                table: "ProductFeatureValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantFeature_ProductFeatureValues_ProductFeatureValueId",
                table: "ProductVariantFeature");

            migrationBuilder.DropTable(
                name: "CategoryFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategories",
                table: "ProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductFeatureValues",
                table: "ProductFeatureValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatureValues_CategoryFeatureId",
                table: "ProductFeatureValues");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "CategoryFeatureId",
                table: "ProductFeatureValues");

            migrationBuilder.RenameTable(
                name: "ProductCategories",
                newName: "productCategories");

            migrationBuilder.RenameTable(
                name: "ProductFeatureValues",
                newName: "ProductFeatureValue");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategories_ParentId",
                table: "productCategories",
                newName: "IX_productCategories_ParentId");

            migrationBuilder.RenameColumn(
                name: "FeatureId",
                table: "ProductFeatureValue",
                newName: "ProductFeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFeatureValues_FeatureId",
                table: "ProductFeatureValue",
                newName: "IX_ProductFeatureValue_ProductFeatureId");

            migrationBuilder.AlterColumn<string>(
                name: "MetaKeywords",
                table: "ProductSEOs",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_productCategories",
                table: "productCategories",
                column: "CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductFeatureValue",
                table: "ProductFeatureValue",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeature", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_productCategories_productCategories_ParentId",
                table: "productCategories",
                column: "ParentId",
                principalTable: "productCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatureValue_ProductFeature_ProductFeatureId",
                table: "ProductFeatureValue",
                column: "ProductFeatureId",
                principalTable: "ProductFeature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantFeature_ProductFeatureValue_ProductFeatureValueId",
                table: "ProductVariantFeature",
                column: "ProductFeatureValueId",
                principalTable: "ProductFeatureValue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
