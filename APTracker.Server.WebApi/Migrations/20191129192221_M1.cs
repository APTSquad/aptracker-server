using Microsoft.EntityFrameworkCore.Migrations;

namespace APTracker.Server.WebApi.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                "Rate",
                "Users",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Rate",
                "Users");
        }
    }
}