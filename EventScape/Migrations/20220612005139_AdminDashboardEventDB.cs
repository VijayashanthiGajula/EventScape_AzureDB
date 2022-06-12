using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventScape.Migrations
{
    public partial class AdminDashboardEventDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQueries_AdminDashboardViewModel_AdminDashboardViewModelId",
                table: "UserQueries");

            migrationBuilder.DropIndex(
                name: "IX_UserQueries_AdminDashboardViewModelId",
                table: "UserQueries");

            migrationBuilder.DropColumn(
                name: "AdminDashboardViewModelId",
                table: "UserQueries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminDashboardViewModelId",
                table: "UserQueries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserQueries_AdminDashboardViewModelId",
                table: "UserQueries",
                column: "AdminDashboardViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQueries_AdminDashboardViewModel_AdminDashboardViewModelId",
                table: "UserQueries",
                column: "AdminDashboardViewModelId",
                principalTable: "AdminDashboardViewModel",
                principalColumn: "Id");
        }
    }
}
