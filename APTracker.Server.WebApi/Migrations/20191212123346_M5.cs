using Microsoft.EntityFrameworkCore.Migrations;

namespace APTracker.Server.WebApi.Migrations
{
    public partial class M5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bags_Users_ResponsibleId",
                table: "Bags");

            migrationBuilder.AlterColumn<long>(
                name: "ResponsibleId",
                table: "Bags",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Bags_Users_ResponsibleId",
                table: "Bags",
                column: "ResponsibleId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bags_Users_ResponsibleId",
                table: "Bags");

            migrationBuilder.AlterColumn<long>(
                name: "ResponsibleId",
                table: "Bags",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bags_Users_ResponsibleId",
                table: "Bags",
                column: "ResponsibleId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
