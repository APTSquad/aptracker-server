using Microsoft.EntityFrameworkCore.Migrations;

namespace APTracker.Server.WebApi.Migrations
{
    public partial class M4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Bags_Users_ResponsibleId",
                "Bags");

            migrationBuilder.AlterColumn<long>(
                "ResponsibleId",
                "Bags",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                "FK_Bags_Users_ResponsibleId",
                "Bags",
                "ResponsibleId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                "FK_Bags_Users_ResponsibleId",
                "Bags",
                "ResponsibleId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}