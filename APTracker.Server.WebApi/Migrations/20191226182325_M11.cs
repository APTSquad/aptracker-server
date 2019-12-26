using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APTracker.Server.WebApi.Migrations
{
    public partial class M11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Saved",
                table: "DailyReports");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEdited",
                table: "DailyReports",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "DailyReports",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastEdited",
                table: "DailyReports");

            migrationBuilder.DropColumn(
                name: "State",
                table: "DailyReports");

            migrationBuilder.AddColumn<DateTime>(
                name: "Saved",
                table: "DailyReports",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
