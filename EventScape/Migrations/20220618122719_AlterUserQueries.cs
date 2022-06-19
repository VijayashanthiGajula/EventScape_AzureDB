using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventScape.Migrations
{
    public partial class AlterUserQueries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQueries_AspNetUsers_ApplicationUserId",
                table: "UserQueries");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQueries_Events_EventsID",
                table: "UserQueries");

            migrationBuilder.DropIndex(
                name: "IX_UserQueries_ApplicationUserId",
                table: "UserQueries");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserQueries");

            migrationBuilder.RenameColumn(
                name: "EventsID",
                table: "UserQueries",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_UserQueries_EventsID",
                table: "UserQueries",
                newName: "IX_UserQueries_EventId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserQueries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserQueries_UserId",
                table: "UserQueries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQueries_AspNetUsers_UserId",
                table: "UserQueries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQueries_Events_EventId",
                table: "UserQueries",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQueries_AspNetUsers_UserId",
                table: "UserQueries");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQueries_Events_EventId",
                table: "UserQueries");

            migrationBuilder.DropIndex(
                name: "IX_UserQueries_UserId",
                table: "UserQueries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserQueries");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "UserQueries",
                newName: "EventsID");

            migrationBuilder.RenameIndex(
                name: "IX_UserQueries_EventId",
                table: "UserQueries",
                newName: "IX_UserQueries_EventsID");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserQueries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserQueries_ApplicationUserId",
                table: "UserQueries",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQueries_AspNetUsers_ApplicationUserId",
                table: "UserQueries",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQueries_Events_EventsID",
                table: "UserQueries",
                column: "EventsID",
                principalTable: "Events",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
