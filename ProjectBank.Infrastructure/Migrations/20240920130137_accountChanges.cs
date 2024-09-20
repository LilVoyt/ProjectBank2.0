using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class accountChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Account",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Login",
                table: "Account",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Account_Login",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Account");
        }
    }
}
