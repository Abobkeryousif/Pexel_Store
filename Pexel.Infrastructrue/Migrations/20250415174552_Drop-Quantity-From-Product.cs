using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pexel.Infrastructrue.Migrations
{
    /// <inheritdoc />
    public partial class DropQuantityFromProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
