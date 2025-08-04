using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DoAnWebNC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectDatabase07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 35, 5, "Không dây/Tốc độ cao", "/Image/mouse1.png", "Chuột ASUS ROG Strix Impact", 1090000m },
                    { 36, 5, "Có dây X2H Medium White", "/Image/mouse1.png", "Chuột Pulsar ", 1249000m },
                    { 37, 5, "RGB Superlight Wireless Pink", "/Image/mouse3.png", "Chuột DareU EM901X", 680000m },
                    { 38, 5, "RZ01-03850100-R3M1", "/Image/mouse4.png", "Chuột Razer DeathAdder Essential ", 400000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38);
        }
    }
}
