using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acacia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePublicId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "Companies");
        }
    }
}
