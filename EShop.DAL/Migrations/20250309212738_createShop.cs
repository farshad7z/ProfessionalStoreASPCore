using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class createShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasShop",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LastLogin",
                table: "Users",
                newName: "LastLoginDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "RegistrationDate");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployeeShop",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployeeSite",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    ShopId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopNameFa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ShopNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FullAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.ShopId);
                    table.ForeignKey(
                        name: "FK_Shops_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SecondaryPhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    BankAccountIBAN = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId");
                });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 9,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "CanAddProducts", "افزودن محصولات فروشگاه" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 10,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "ManageAccessUsers", "مدیریت دسترسی کاربران فروشگاه" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 11,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanManageShops", "مدیر سایت - مدیریت فروشگاه‌ها" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 12,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanEditShops", "مدیر سایت - ویرایش فروشگاه‌ها" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 13,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanRemoveShops", "مدیر سایت - حذف فروشگاه‌ها" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 14,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanEditProducts", "مدیر سایت - ویرایش محصولات" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 15,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanRemoveProducts", "مدیر سایت - حذف محصولات" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 16,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanManageOrders", "مدیر سایت - مدیریت سفارشات" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 17,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanAddUsers", "مدیر سایت - افزودن کاربران" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 18,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanEditUsers", "مدیر سایت - ویرایش کاربران" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 19,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanRemoveUsers", "مدیر سایت - حذف کاربران" });

            migrationBuilder.InsertData(
                table: "AppClaims",
                columns: new[] { "ClaimId", "ClaimType", "Value" },
                values: new object[,]
                {
                    { 20, "AdminCanReplayComments", "مدیر سایت - پاسخ به نظرات" },
                    { 21, "AdminManageAccessUsers", "مدیر سایت - مدیریت دسترسی کاربران" }
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "ShopId", "Description", "Email", "FullAddress", "IsActive", "IsDeleted", "LastUpdated", "Latitude", "Longitude", "OwnerId", "PhoneNumber", "PostalCode", "RegistrationDate", "ShopNameEn", "ShopNameFa" },
                values: new object[] { 1, "فروشگاه اینترنتی مدرن با تنوع بالا در محصولات الکترونیکی، پوشاک و لوازم خانگی. ارائه دهنده بهترین قیمت‌ها با تضمین کیفیت!", "BazarPal_info@gmail.com", "زنجان، ابهر، خیابان اصلی، پلاک 21", true, false, null, 36.1468m, 49.2332m, 1, "09109999414", "445452654", new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "BazarPal", "بازارپال" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IsDeleted", "IsEmployeeShop", "IsEmployeeSite", "RegistrationDate" },
                values: new object[] { false, true, true, new DateTime(2025, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Address", "BankAccountIBAN", "CreatedAt", "IsActive", "IsDeleted", "NationalCode", "PasswordHash", "SecondaryPhoneNumber", "ShopId", "UpdatedAt", "UserId" },
                values: new object[] { 1, "زنجان، ابهر، خیابان اصلی، پلاک ۱۲", "IR021000001330000544121545", new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "4400230147", null, "09058794262", 1, null, 1 });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "ClaimId", "UserId", "ClaimValue" },
                values: new object[,]
                {
                    { 20, 1, "CanManageShop" },
                    { 21, 1, "CanEditProducts" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ShopId",
                table: "Employees",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ShopId",
                table: "Product",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_OwnerId",
                table: "Shops",
                column: "OwnerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumns: new[] { "ClaimId", "UserId" },
                keyValues: new object[] { 20, 1 });

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumns: new[] { "ClaimId", "UserId" },
                keyValues: new object[] { 21, 1 });

            migrationBuilder.DeleteData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 21);

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEmployeeShop",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEmployeeSite",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastLoginDate",
                table: "Users",
                newName: "LastLogin");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AddColumn<bool>(
                name: "HasShop",
                table: "Users",
                type: "bit",
                maxLength: 300,
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 9,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanManageShops", "مدیر سایت - مدیریت فروشگاه‌ها" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 10,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanEditShops", "مدیر سایت - ویرایش فروشگاه‌ها" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 11,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanRemoveShops", "مدیر سایت - حذف فروشگاه‌ها" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 12,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanEditProducts", "مدیر سایت - ویرایش محصولات" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 13,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanRemoveProducts", "مدیر سایت - حذف محصولات" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 14,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanManageOrders", "مدیر سایت - مدیریت سفارشات" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 15,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanAddUsers", "مدیر سایت - افزودن کاربران" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 16,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanEditUsers", "مدیر سایت - ویرایش کاربران" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 17,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanRemoveUsers", "مدیر سایت - حذف کاربران" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 18,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminCanReplayComments", "مدیر سایت - پاسخ به نظرات" });

            migrationBuilder.UpdateData(
                table: "AppClaims",
                keyColumn: "ClaimId",
                keyValue: 19,
                columns: new[] { "ClaimType", "Value" },
                values: new object[] { "AdminManageAccessUsers", "مدیر سایت - مدیریت دسترسی کاربران" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "HasShop" },
                values: new object[] { new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), true });
        }
    }
}
