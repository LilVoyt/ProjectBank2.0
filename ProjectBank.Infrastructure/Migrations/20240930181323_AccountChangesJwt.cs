using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBank.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class AccountChangesJwt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Account",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Account",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Account");
        }
    }
}
