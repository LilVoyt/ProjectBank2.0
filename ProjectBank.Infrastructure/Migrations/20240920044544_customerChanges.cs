using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBank.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class customerChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Customer",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customer",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Customer",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Customer",
                newName: "Name");
        }
    }
}
