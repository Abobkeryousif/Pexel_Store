using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pexel.Infrastructrue.Migrations
{
    /// <inheritdoc />
    public partial class AddImageTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ImageName = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ImageName = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
