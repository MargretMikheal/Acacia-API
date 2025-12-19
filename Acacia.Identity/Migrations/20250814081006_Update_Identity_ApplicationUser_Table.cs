using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Acacia.Identity.Migrations
{
    /// <inheritdoc />
    public partial class Update_Identity_ApplicationUser_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cbc43a8e-f7bb-4445-baaf-1add431ffbbf", "7e445865-a24d-4543-b6c6-9443d048cdb9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", "9e224968-33e4-4652-b7b7-8574d048cdb9" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e445865-a24d-4543-b6c6-9443d048cdb9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Country", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastLoginDate", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7e445865-a24d-4543-b6c6-9443d048cdb9", 0, "123 Nasser Street, Caito City, GC 12345", "c27d4b01-88f5-485b-8b56-f3324ca956a8", "Egypt", new DateTime(2025, 8, 11, 10, 37, 40, 574, DateTimeKind.Utc).AddTicks(948), "admin@acacia.com", true, "System", null, "Admin", false, null, "ADMIN@ACACIA.COM", "ADMIN@ACACIA.COM", "AQAAAAIAAYagAAAAEIueYJFXZvDVbR1Hej5lFWNIDKQyeFbnvV8RiS6ab9sH7zzdAno4Js1iyGCzR0G0tQ==", null, false, "c88c1084-e82c-4611-a217-397b403608a5", false, "admin@acacia" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "123 Nasser Street, Caito City, GC 12345", "771a185c-c780-4e03-b371-cf0f228cf1b7", "Egypt", new DateTime(2025, 8, 11, 10, 37, 40, 529, DateTimeKind.Utc).AddTicks(7226), "user@acacia.com", true, "System", null, "User", false, null, "USER@ACACIA.COM", "USER@GAMEIT.COM", "AQAAAAIAAYagAAAAEKvz8u7N6xolDPcqrpQsG/SaP4nAkAbR+R8INA4JryHexuhHHg90C8ljq9I/jv+vgw==", null, false, "4b5cc570-b801-4de3-91c9-5d843f57bd3e", false, "user@acacia" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "cbc43a8e-f7bb-4445-baaf-1add431ffbbf", "7e445865-a24d-4543-b6c6-9443d048cdb9" },
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", "9e224968-33e4-4652-b7b7-8574d048cdb9" }
                });
        }
    }
}
