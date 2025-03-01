using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateClims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ActiveCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasShop",
                table: "Users",
                type: "bit",
                maxLength: 300,
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AppClaims",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppClaims", x => x.ClaimId);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => new { x.UserId, x.ClaimId });
                    table.ForeignKey(
                        name: "FK_UserClaims_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Claims",
                        column: x => x.ClaimId,
                        principalTable: "AppClaims",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AppClaims",
                columns: new[] { "ClaimId", "ClaimType", "Value" },
                values: new object[,]
                {
                    { 1, "CanManageUsers", "مدیریت کارمندان فروشگاه" },
                    { 2, "CanAddUsersOnShop", "اضافه کردن کارمند به فروشگاه" },
                    { 3, "CanRemoveUsersOnShop", "حذف کارمند از فروشگاه" },
                    { 4, "CanManageShop", "دسترسی به مدیریت فروشگاه" },
                    { 5, "CanEditProducts", "دسترسی به ویرایش محصولات" },
                    { 6, "CanRemoveProducts", "دسترسی به حذف محصولات" },
                    { 7, "CanManageOrders", "دسترسی به مدیریت سفارش‌ها" },
                    { 8, "CanManageContent", "دسترسی به مدیریت محتوا" },
                    { 9, "AdminCanManageShops", "مدیر سایت - مدیریت فروشگاه‌ها" },
                    { 10, "AdminCanEditShops", "مدیر سایت - ویرایش فروشگاه‌ها" },
                    { 11, "AdminCanRemoveShops", "مدیر سایت - حذف فروشگاه‌ها" },
                    { 12, "AdminCanEditProducts", "مدیر سایت - ویرایش محصولات" },
                    { 13, "AdminCanRemoveProducts", "مدیر سایت - حذف محصولات" },
                    { 14, "AdminCanManageOrders", "مدیر سایت - مدیریت سفارشات" },
                    { 15, "AdminCanAddUsers", "مدیر سایت - افزودن کاربران" },
                    { 16, "AdminCanEditUsers", "مدیر سایت - ویرایش کاربران" },
                    { 17, "AdminCanRemoveUsers", "مدیر سایت - حذف کاربران" },
                    { 18, "AdminCanReplayComments", "مدیر سایت - پاسخ به نظرات" },
                    { 19, "AdminManageAccessUsers", "مدیر سایت - مدیریت دسترسی کاربران" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "HasShop",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_ClaimId",
                table: "UserClaims",
                column: "ClaimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "AppClaims");

            migrationBuilder.DropColumn(
                name: "HasShop",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "ActiveCode",
                table: "Users",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
