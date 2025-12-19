using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acacia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSizeUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeUnit",
                table: "ProductSizes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SizeUnit",
                table: "ProductSizes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
