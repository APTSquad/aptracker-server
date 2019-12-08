using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace APTracker.Server.WebApi.Migrations
{
    public partial class M3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Bags",
                table => new
                {
                    Id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    ResponsibleId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bags", x => x.Id);
                    table.ForeignKey(
                        "FK_Bags_Users_ResponsibleId",
                        x => x.ResponsibleId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "DailyReports",
                table => new
                {
                    Id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(),
                    Saved = table.Column<DateTime>()
                },
                constraints: table => { table.PrimaryKey("PK_DailyReports", x => x.Id); });

            migrationBuilder.CreateTable(
                "Clients",
                table => new
                {
                    Id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    BagId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        "FK_Clients_Bags_BagId",
                        x => x.BagId,
                        "Bags",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Projects",
                table => new
                {
                    Id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    ClientId = table.Column<long>(nullable: true),
                    BagId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        "FK_Projects_Bags_BagId",
                        x => x.BagId,
                        "Bags",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Projects_Clients_ClientId",
                        x => x.ClientId,
                        "Clients",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "ConsumptionArticles",
                table => new
                {
                    Id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsActive = table.Column<bool>(),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    BagId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionArticles", x => x.Id);
                    table.ForeignKey(
                        "FK_ConsumptionArticles_Bags_BagId",
                        x => x.BagId,
                        "Bags",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_ConsumptionArticles_Projects_ProjectId",
                        x => x.ProjectId,
                        "Projects",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "ConsumptionReportItems",
                table => new
                {
                    Id = table.Column<long>()
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HoursConsumption = table.Column<double>(),
                    ArticleId = table.Column<long>(nullable: true),
                    DailyReportId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionReportItems", x => x.Id);
                    table.ForeignKey(
                        "FK_ConsumptionReportItems_ConsumptionArticles_ArticleId",
                        x => x.ArticleId,
                        "ConsumptionArticles",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_ConsumptionReportItems_DailyReports_DailyReportId",
                        x => x.DailyReportId,
                        "DailyReports",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Bags_ResponsibleId",
                "Bags",
                "ResponsibleId");

            migrationBuilder.CreateIndex(
                "IX_Clients_BagId",
                "Clients",
                "BagId");

            migrationBuilder.CreateIndex(
                "IX_ConsumptionArticles_BagId",
                "ConsumptionArticles",
                "BagId");

            migrationBuilder.CreateIndex(
                "IX_ConsumptionArticles_ProjectId",
                "ConsumptionArticles",
                "ProjectId");

            migrationBuilder.CreateIndex(
                "IX_ConsumptionReportItems_ArticleId",
                "ConsumptionReportItems",
                "ArticleId");

            migrationBuilder.CreateIndex(
                "IX_ConsumptionReportItems_DailyReportId",
                "ConsumptionReportItems",
                "DailyReportId");

            migrationBuilder.CreateIndex(
                "IX_Projects_BagId",
                "Projects",
                "BagId");

            migrationBuilder.CreateIndex(
                "IX_Projects_ClientId",
                "Projects",
                "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "ConsumptionReportItems");

            migrationBuilder.DropTable(
                "ConsumptionArticles");

            migrationBuilder.DropTable(
                "DailyReports");

            migrationBuilder.DropTable(
                "Projects");

            migrationBuilder.DropTable(
                "Clients");

            migrationBuilder.DropTable(
                "Bags");
        }
    }
}