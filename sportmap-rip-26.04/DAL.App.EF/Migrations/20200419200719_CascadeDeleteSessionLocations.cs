using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class CascadeDeleteSessionLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GpsLocations_GpsSessions_GpsSessionId",
                table: "GpsLocations");

            migrationBuilder.AddForeignKey(
                name: "FK_GpsLocations_GpsSessions_GpsSessionId",
                table: "GpsLocations",
                column: "GpsSessionId",
                principalTable: "GpsSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GpsLocations_GpsSessions_GpsSessionId",
                table: "GpsLocations");

            migrationBuilder.AddForeignKey(
                name: "FK_GpsLocations_GpsSessions_GpsSessionId",
                table: "GpsLocations",
                column: "GpsSessionId",
                principalTable: "GpsSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
