using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APTracker.Server.WebApi.Migrations
{
    public partial class M10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_DailyReports_Day_Month_Year_UserId",
                "DailyReports");

            migrationBuilder.DropColumn(
                "Day",
                "DailyReports");

            migrationBuilder.DropColumn(
                "Month",
                "DailyReports");

            migrationBuilder.DropColumn(
                "Year",
                "DailyReports");

            migrationBuilder.AddColumn<DateTime>(
                "Date",
                "DailyReports",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                "IX_DailyReports_Date_UserId",
                "DailyReports",
                new[] {"Date", "UserId"},
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_DailyReports_Date_UserId",
                "DailyReports");

            migrationBuilder.DropColumn(
                "Date",
                "DailyReports");

            migrationBuilder.AddColumn<int>(
                "Day",
                "DailyReports",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "Month",
                "DailyReports",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "Year",
                "DailyReports",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_DailyReports_Day_Month_Year_UserId",
                "DailyReports",
                new[] {"Day", "Month", "Year", "UserId"},
                unique: true);
        }
    }
}