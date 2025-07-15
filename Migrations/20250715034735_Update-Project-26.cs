using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnWebNC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProject26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 3, 1, "i5-13500H/512GB/16GB", "/Image/laptop3.png", "Laptop ASUS ExpertBook", 17000000m });
        }
    }
}
