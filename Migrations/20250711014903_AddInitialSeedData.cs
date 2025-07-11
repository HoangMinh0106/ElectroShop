using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DoAnWebNC.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Laptop" },
                    { 2, null, "Laptop Gaming" },
                    { 3, null, "Màn hình" },
                    { 4, null, "Bàn phím" }
                });

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "Id", "Code", "DiscountType", "DiscountValue", "ExpiryDate", "IsActive", "MinAmount" },
                values: new object[,]
                {
                    { 1, "SALE25", 1, 25m, new DateTime(2025, 12, 31, 23, 59, 59, 0, DateTimeKind.Unspecified), true, 200000m },
                    { 2, "WELCOME50K", 0, 50000m, new DateTime(2025, 10, 11, 23, 59, 59, 0, DateTimeKind.Unspecified), true, 300000m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "i5-13420H/512GB/24GB", "/Image/laptop1.png", "Laptop Lenovo IdeaPad", 15000000m },
                    { 2, 1, "i5-1235U/512GB/14GB", "/Image/laptop2.png", "Laptop Avita PURA", 16000000m },
                    { 3, 1, "i5-13500H/512GB/16GB", "/Image/laptop3.png", "Laptop ASUS ExpertBook", 17000000m },
                    { 4, 1, "i5-13500H/512GB/16GB", "/Image/laptop4.png", "Laptop Lenovo ThinkBook", 15500000m },
                    { 5, 1, "i5-12500H/512GB/16GB", "/Image/laptop5.png", "Laptop ASUS Vivobook", 18000000m },
                    { 6, 1, "i3-1305U/512GB/8GB", "/Image/laptop6.png", "Laptop Dell Inspiron", 16500000m },
                    { 7, 1, "i5-13420H/512GB/8GB", "/Image/laptop7.png", "Laptop Dell XPS", 19000000m },
                    { 8, 1, "Ultra-7-155H/512GB/16GB", "/Image/laptop8.png", "Laptop LG Gram", 17500000m },
                    { 9, 1, "Ultra-5-125H/512GB/16GB", "/Image/laptop9.png", "Laptop MSI Prestige", 20000000m },
                    { 10, 1, "Ultra-7-258V/512GB/1TB", "/Image/laptop10.png", "Laptop Lenovo Yoga", 21000000m },
                    { 11, 2, "Laptop gaming cấu hình khủng 1", "/images/gaming1.jpg", "Laptop Gaming 1", 22000000m },
                    { 12, 2, "Laptop gaming cấu hình khủng 2", "/images/gaming2.jpg", "Laptop Gaming 2", 23000000m },
                    { 13, 2, "Laptop gaming cấu hình khủng 3", "/images/gaming3.jpg", "Laptop Gaming 3", 24000000m },
                    { 14, 2, "Laptop gaming cấu hình khủng 4", "/images/gaming4.jpg", "Laptop Gaming 4", 25000000m },
                    { 15, 2, "Laptop gaming cấu hình khủng 5", "/images/gaming5.jpg", "Laptop Gaming 5", 26000000m },
                    { 16, 2, "Laptop gaming cấu hình khủng 6", "/images/gaming6.jpg", "Laptop Gaming 6", 27000000m },
                    { 17, 2, "Laptop gaming cấu hình khủng 7", "/images/gaming7.jpg", "Laptop Gaming 7", 28000000m },
                    { 18, 2, "Laptop gaming cấu hình khủng 8", "/images/gaming8.jpg", "Laptop Gaming 8", 29000000m },
                    { 19, 2, "Laptop gaming cấu hình khủng 9", "/images/gaming9.jpg", "Laptop Gaming 9", 30000000m },
                    { 20, 2, "Laptop gaming cấu hình khủng 10", "/images/gaming10.jpg", "Laptop Gaming 10", 31000000m },
                    { 21, 3, "Màn hình sắc nét 1", "/images/monitor1.jpg", "Màn hình 1", 4000000m },
                    { 22, 3, "Màn hình sắc nét 2", "/images/monitor2.jpg", "Màn hình 2", 4200000m },
                    { 23, 3, "Màn hình sắc nét 3", "/images/monitor3.jpg", "Màn hình 3", 4300000m },
                    { 24, 3, "Màn hình sắc nét 4", "/images/monitor4.jpg", "Màn hình 4", 4400000m },
                    { 25, 3, "Màn hình sắc nét 5", "/images/monitor5.jpg", "Màn hình 5", 4600000m },
                    { 26, 3, "Màn hình sắc nét 6", "/images/monitor6.jpg", "Màn hình 6", 4700000m },
                    { 27, 3, "Màn hình sắc nét 7", "/images/monitor7.jpg", "Màn hình 7", 4500000m },
                    { 28, 3, "Màn hình sắc nét 8", "/images/monitor8.jpg", "Màn hình 8", 4800000m },
                    { 29, 3, "Màn hình sắc nét 9", "/images/monitor9.jpg", "Màn hình 9", 4900000m },
                    { 30, 3, "Màn hình sắc nét 10", "/images/monitor10.jpg", "Màn hình 10", 5000000m },
                    { 31, 4, "Bàn phím cơ học 1", "/images/keyboard1.jpg", "Bàn phím 1", 800000m },
                    { 32, 4, "Bàn phím cơ học 2", "/images/keyboard2.jpg", "Bàn phím 2", 850000m },
                    { 33, 4, "Bàn phím cơ học 3", "/images/keyboard3.jpg", "Bàn phím 3", 820000m },
                    { 34, 4, "Bàn phím cơ học 4", "/images/keyboard4.jpg", "Bàn phím 4", 880000m },
                    { 35, 4, "Bàn phím cơ học 5", "/images/keyboard5.jpg", "Bàn phím 5", 900000m },
                    { 36, 4, "Bàn phím cơ học 6", "/images/keyboard6.jpg", "Bàn phím 6", 920000m },
                    { 37, 4, "Bàn phím cơ học 7", "/images/keyboard7.jpg", "Bàn phím 7", 950000m },
                    { 38, 4, "Bàn phím cơ học 8", "/images/keyboard8.jpg", "Bàn phím 8", 970000m },
                    { 39, 4, "Bàn phím cơ học 9", "/images/keyboard9.jpg", "Bàn phím 9", 990000m },
                    { 40, 4, "Bàn phím cơ học 10", "/images/keyboard10.jpg", "Bàn phím 10", 1000000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

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
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

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
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34);

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

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
