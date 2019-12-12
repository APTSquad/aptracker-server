using Microsoft.EntityFrameworkCore.Migrations;

namespace APTracker.Server.WebApi.Migrations
{
    public partial class M5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Bags_Users_ResponsibleId",
                "Bags");

            migrationBuilder.AlterColumn<long>(
                "ResponsibleId",
                "Bags",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                "FK_Bags_Users_ResponsibleId",
                "Bags",
                "ResponsibleId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Bags_Users_ResponsibleId",
                "Bags");

            migrationBuilder.AlterColumn<long>(
                "ResponsibleId",
                "Bags",
                "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                "FK_Bags_Users_ResponsibleId",
                "Bags",
                "ResponsibleId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}