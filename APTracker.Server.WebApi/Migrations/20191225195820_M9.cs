using Microsoft.EntityFrameworkCore.Migrations;

namespace APTracker.Server.WebApi.Migrations
{
    public partial class M9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_ConsumptionReportItems_ConsumptionArticles_ArticleId",
                "ConsumptionReportItems");

            migrationBuilder.DropForeignKey(
                "FK_DailyReports_Users_UserId",
                "DailyReports");

            migrationBuilder.AlterColumn<long>(
                "UserId",
                "DailyReports",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                "ArticleId",
                "ConsumptionReportItems",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                "IsCommon",
                "ConsumptionArticles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                "FK_ConsumptionReportItems_ConsumptionArticles_ArticleId",
                "ConsumptionReportItems",
                "ArticleId",
                "ConsumptionArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_DailyReports_Users_UserId",
                "DailyReports",
                "UserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_ConsumptionReportItems_ConsumptionArticles_ArticleId",
                "ConsumptionReportItems");

            migrationBuilder.DropForeignKey(
                "FK_DailyReports_Users_UserId",
                "DailyReports");

            migrationBuilder.DropColumn(
                "IsCommon",
                "ConsumptionArticles");

            migrationBuilder.AlterColumn<long>(
                "UserId",
                "DailyReports",
                "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                "ArticleId",
                "ConsumptionReportItems",
                "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                "FK_ConsumptionReportItems_ConsumptionArticles_ArticleId",
                "ConsumptionReportItems",
                "ArticleId",
                "ConsumptionArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_DailyReports_Users_UserId",
                "DailyReports",
                "UserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}