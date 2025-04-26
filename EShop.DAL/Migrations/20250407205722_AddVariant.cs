using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddVariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_Products_ProductId",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantFeature_ProductFeatureValues_ProductFeatureValueId",
                table: "ProductVariantFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantFeature_ProductVariant_ProductVariantId",
                table: "ProductVariantFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantFeature",
                table: "ProductVariantFeature");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariantFeature_ProductVariantId",
                table: "ProductVariantFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant");

            migrationBuilder.RenameTable(
                name: "ProductVariantFeature",
                newName: "ProductVariantFeatures");

            migrationBuilder.RenameTable(
                name: "ProductVariant",
                newName: "ProductVariants");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantFeature_ProductFeatureValueId",
                table: "ProductVariantFeatures",
                newName: "IX_ProductVariantFeatures_ProductFeatureValueId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariants",
                newName: "IX_ProductVariants_ProductId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAvailable",
                table: "ProductVariants",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantFeatures",
                table: "ProductVariantFeatures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantFeatures_ProductVariantId_ProductFeatureValueId",
                table: "ProductVariantFeatures",
                columns: new[] { "ProductVariantId", "ProductFeatureValueId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantFeatures_ProductFeatureValues_ProductFeatureValueId",
                table: "ProductVariantFeatures",
                column: "ProductFeatureValueId",
                principalTable: "ProductFeatureValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantFeatures_ProductVariants_ProductVariantId",
                table: "ProductVariantFeatures",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantFeatures_ProductFeatureValues_ProductFeatureValueId",
                table: "ProductVariantFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantFeatures_ProductVariants_ProductVariantId",
                table: "ProductVariantFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantFeatures",
                table: "ProductVariantFeatures");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariantFeatures_ProductVariantId_ProductFeatureValueId",
                table: "ProductVariantFeatures");

            migrationBuilder.RenameTable(
                name: "ProductVariants",
                newName: "ProductVariant");

            migrationBuilder.RenameTable(
                name: "ProductVariantFeatures",
                newName: "ProductVariantFeature");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariant",
                newName: "IX_ProductVariant_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantFeatures_ProductFeatureValueId",
                table: "ProductVariantFeature",
                newName: "IX_ProductVariantFeature_ProductFeatureValueId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAvailable",
                table: "ProductVariant",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantFeature",
                table: "ProductVariantFeature",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantFeature_ProductVariantId",
                table: "ProductVariantFeature",
                column: "ProductVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_Products_ProductId",
                table: "ProductVariant",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantFeature_ProductFeatureValues_ProductFeatureValueId",
                table: "ProductVariantFeature",
                column: "ProductFeatureValueId",
                principalTable: "ProductFeatureValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantFeature_ProductVariant_ProductVariantId",
                table: "ProductVariantFeature",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
