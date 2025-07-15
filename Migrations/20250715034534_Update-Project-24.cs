using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DoAnWebNC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProject24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 5, 1, "i5-12500H/512GB/16GB", "/Image/laptop5.png", "Laptop ASUS Vivobook", 18000000m },
                    { 6, 1, "i3-1305U/512GB/8GB", "/Image/laptop6.png", "Laptop Dell Inspiron", 16500000m },
                    { 8, 1, "Ultra-7-155H/512GB/16GB", "/Image/laptop8.png", "Laptop LG Gram", 17500000m }
                });
        }
    }
}
