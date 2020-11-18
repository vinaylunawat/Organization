using Microsoft.EntityFrameworkCore.Migrations;

namespace Geography.Migrations.Migrations
{
    public partial class M20201118T2301299552designer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Test",
                schema: "Geography",
                table: "Country",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                schema: "Geography",
                table: "Country");
        }
    }
}
