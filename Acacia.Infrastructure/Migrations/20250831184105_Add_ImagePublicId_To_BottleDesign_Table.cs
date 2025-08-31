using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acacia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_ImagePublicId_To_BottleDesign_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "BottleDesigns",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "BottleDesigns");
        }
    }
}
