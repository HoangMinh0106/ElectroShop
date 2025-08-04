using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DoAnWebNC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProjectDatabase06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

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

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "25inch/180Hz", "/Image/monitor1.png", "Màn hình Asus TUF GAMING" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "22inch/100Hz", "/Image/monitor2.png", "Màn hình ViewSonic VA2215-H" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "23inch/200Hz", "/Image/monitor3.png", "Màn hình Acer KG240Y-X1" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "27inch/165Hz", "/Image/monitor4.png", "Màn hình LG 27GR75Q-B" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Hồng gradient/Black King magnetic switch", "/Image/keyboard1.png", "Bàn phím AULA HERO68HE", 900000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Plus Black & Gold Fairy (Silent) switch", "/Image/keyboard2.png", "Bàn phím AKKO 3098B" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Trắng/Light feather switch", "/Image/keyboard3.png", "Bàn phím Leobog AMG65 TM" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Space Gray Scissor", "/Image/keyboard4.png", "Bàn phím Keychron B1P" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Màn hình sắc nét 1", "/images/monitor1.jpg", "Màn hình 1" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Màn hình sắc nét 2", "/images/monitor2.jpg", "Màn hình 2" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Màn hình sắc nét 3", "/images/monitor3.jpg", "Màn hình 3" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Màn hình sắc nét 4", "/images/monitor4.jpg", "Màn hình 4" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Bàn phím cơ học 1", "/images/keyboard1.jpg", "Bàn phím 1", 800000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Bàn phím cơ học 2", "/images/keyboard2.jpg", "Bàn phím 2" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Bàn phím cơ học 3", "/images/keyboard3.jpg", "Bàn phím 3" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Bàn phím cơ học 4", "/images/keyboard4.jpg", "Bàn phím 4" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 18, 2, "i7-12650H/512GB/16GB", "/Image/gaming8.png", "Laptop gaming Acer Aspire 7", 29000000m },
                    { 25, 3, "Màn hình sắc nét 5", "/images/monitor5.jpg", "Màn hình 5", 4600000m },
                    { 26, 3, "Màn hình sắc nét 6", "/images/monitor6.jpg", "Màn hình 6", 4700000m },
                    { 27, 3, "Màn hình sắc nét 7", "/images/monitor7.jpg", "Màn hình 7", 4500000m },
                    { 28, 3, "Màn hình sắc nét 8", "/images/monitor8.jpg", "Màn hình 8", 4800000m },
                    { 35, 4, "Bàn phím cơ học 5", "/images/keyboard5.jpg", "Bàn phím 5", 900000m },
                    { 36, 4, "Bàn phím cơ học 6", "/images/keyboard6.jpg", "Bàn phím 6", 920000m },
                    { 37, 4, "Bàn phím cơ học 7", "/images/keyboard7.jpg", "Bàn phím 7", 950000m },
                    { 38, 4, "Bàn phím cơ học 8", "/images/keyboard8.jpg", "Bàn phím 8", 970000m }
                });
        }
    }
}
