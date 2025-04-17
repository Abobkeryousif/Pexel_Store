using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pexel.Infrastructrue.Migrations
{
    /// <inheritdoc />
    public partial class AddIntImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ImageId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Images",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_Id",
                table: "Images",
                column: "Id",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_Id",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Images",
                newName: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ImageId",
                table: "Images",
                column: "ImageId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
