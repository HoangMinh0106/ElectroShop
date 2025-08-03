using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnWebNC.Migrations
{
    /// <inheritdoc />
    public partial class AddForgotPasswordFields3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetTokenExpiry",
                table: "Users",
                newName: "PasswordResetTokenExpiry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordResetTokenExpiry",
                table: "Users",
                newName: "ResetTokenExpiry");
        }
    }
}
