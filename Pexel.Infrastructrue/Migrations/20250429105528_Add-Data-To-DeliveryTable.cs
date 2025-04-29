using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pexel.Infrastructrue.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToDeliveryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DeliverMethods",
                columns: new[] { "Id", "CompanyName", "DeliveryTime", "Description", "Price" },
                values: new object[,]
                {
                    { 1, "Noon", "Two Days", "Best Delivery Company And Fast", 22m },
                    { 2, "Jahez", "1 Day", "Best Delivery Company And Fast", 19m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliverMethods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeliverMethods",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
