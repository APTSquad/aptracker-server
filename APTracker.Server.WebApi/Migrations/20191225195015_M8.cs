using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APTracker.Server.WebApi.Migrations
{
    public partial class M8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Clients_Bags_BagId",
                "Clients");

            migrationBuilder.DropForeignKey(
                "FK_Projects_Clients_ClientId",
                "Projects");

            migrationBuilder.DropColumn(
                "Date",
                "DailyReports");

            migrationBuilder.AlterColumn<long>(
                "ClientId",
                "Projects",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                "Day",
                "DailyReports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "Month",
                "DailyReports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                "UserId",
                "DailyReports",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Year",
                "DailyReports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                "BagId",
                "Clients",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                "IX_DailyReports_UserId",
                "DailyReports",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_DailyReports_Day_Month_Year_UserId",
                "DailyReports",
                new[] {"Day", "Month", "Year", "UserId"},
                unique: true);

            migrationBuilder.AddForeignKey(
                "FK_Clients_Bags_BagId",
                "Clients",
                "BagId",
                "Bags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_DailyReports_Users_UserId",
                "DailyReports",
                "UserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Projects_Clients_ClientId",
                "Projects",
                "ClientId",
                "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Clients_Bags_BagId",
                "Clients");

            migrationBuilder.DropForeignKey(
                "FK_DailyReports_Users_UserId",
                "DailyReports");

            migrationBuilder.DropForeignKey(
                "FK_Projects_Clients_ClientId",
                "Projects");

            migrationBuilder.DropIndex(
                "IX_DailyReports_UserId",
                "DailyReports");

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
                "UserId",
                "DailyReports");

            migrationBuilder.DropColumn(
                "Year",
                "DailyReports");

            migrationBuilder.AlterColumn<long>(
                "ClientId",
                "Projects",
                "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<DateTime>(
                "Date",
                "DailyReports",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<long>(
                "BagId",
                "Clients",
                "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                "FK_Clients_Bags_BagId",
                "Clients",
                "BagId",
                "Bags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Projects_Clients_ClientId",
                "Projects",
                "ClientId",
                "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}