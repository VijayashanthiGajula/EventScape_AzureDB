﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventScape.Migrations
{
    public partial class AdminDashboardViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AdminDashboardViewModel_AdminDashboardViewModelId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "AdminDashboardViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Events_AdminDashboardViewModelId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AdminDashboardViewModelId",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminDashboardViewModelId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdminDashboardViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalEventsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminDashboardViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_AdminDashboardViewModelId",
                table: "Events",
                column: "AdminDashboardViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AdminDashboardViewModel_AdminDashboardViewModelId",
                table: "Events",
                column: "AdminDashboardViewModelId",
                principalTable: "AdminDashboardViewModel",
                principalColumn: "Id");
        }
    }
}
