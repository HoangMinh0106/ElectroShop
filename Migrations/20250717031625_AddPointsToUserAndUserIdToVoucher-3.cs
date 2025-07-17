using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnWebNC.Migrations
{
    /// <inheritdoc />
    public partial class AddPointsToUserAndUserIdToVoucher3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsActive", "IsTemplate" },
                values: new object[] { false, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vouchers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsActive", "IsTemplate" },
                values: new object[] { true, false });
        }
    }
}
