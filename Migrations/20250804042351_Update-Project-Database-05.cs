using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DoAnWebNC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectDatabase05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "/Image/gaming1.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "/Image/gaming2.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageUrl",
                value: "/Image/gaming3.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "i5-12450HX/512GB/16GB", "/Image/gaming4.png", "Laptop gaming Lenovo LOQ 15IAX9E" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "i5-13420H/512GB/16GB", "/Image/gaming5.png", "Laptop gaming MSI Thin 15" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "i7-13620H/512GB/16GB", "/Image/gaming6.png", "Laptop gaming Gigabyte G5" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "i7-13620H/512GB/8GB", "/Image/gaming7.png", "Laptop gaming MSI Katana 15" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "i7-12650H/512GB/16GB", "/Image/gaming8.png", "Laptop gaming Acer Aspire 7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "/images/gaming1.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "/images/gaming2.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageUrl",
                value: "/images/gaming3.png");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Laptop gaming cấu hình khủng 4", "/images/gaming4.jpg", "Laptop Gaming 4" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Laptop gaming cấu hình khủng 5", "/images/gaming5.jpg", "Laptop Gaming 5" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Laptop gaming cấu hình khủng 6", "/images/gaming6.jpg", "Laptop Gaming 6" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Laptop gaming cấu hình khủng 7", "/images/gaming7.jpg", "Laptop Gaming 7" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Laptop gaming cấu hình khủng 8", "/images/gaming8.jpg", "Laptop Gaming 8" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 29, 3, "Màn hình sắc nét 9", "/images/monitor9.jpg", "Màn hình 9", 4900000m },
                    { 30, 3, "Màn hình sắc nét 10", "/images/monitor10.jpg", "Màn hình 10", 5000000m },
                    { 39, 4, "Bàn phím cơ học 9", "/images/keyboard9.jpg", "Bàn phím 9", 990000m },
                    { 40, 4, "Bàn phím cơ học 10", "/images/keyboard10.jpg", "Bàn phím 10", 1000000m }
                });
        }
    }
}
