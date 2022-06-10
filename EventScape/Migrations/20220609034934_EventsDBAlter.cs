using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventScape.Migrations
{
    public partial class EventsDBAlter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventPosters",
                table: "Events",
                newName: "EventPosterName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventPosterName",
                table: "Events",
                newName: "EventPosters");
        }
    }
}
