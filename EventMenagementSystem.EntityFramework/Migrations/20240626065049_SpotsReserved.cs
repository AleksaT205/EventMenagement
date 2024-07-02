using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventMenagementSystem.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class SpotsReserved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpotsReserved",
                table: "UserEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpotsReserved",
                table: "UserEvents");
        }
    }
}
