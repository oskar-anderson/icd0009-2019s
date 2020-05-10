using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class RenameSpeedToPace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PaceMax",
                table: "GpsSessions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PaceMin",
                table: "GpsSessions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.Sql(@"UPDATE GpsSessions SET PaceMax = MaxSpeed, PaceMin = MinSpeed;");
            
            migrationBuilder.DropColumn(
                name: "MaxSpeed",
                table: "GpsSessions");

            migrationBuilder.DropColumn(
                name: "MinSpeed",
                table: "GpsSessions");


            migrationBuilder.CreateIndex(
                name: "IX_GpsSessions_CreatedAt",
                table: "GpsSessions",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_GpsLocations_CreatedAt",
                table: "GpsLocations",
                column: "CreatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GpsSessions_CreatedAt",
                table: "GpsSessions");

            migrationBuilder.DropIndex(
                name: "IX_GpsLocations_CreatedAt",
                table: "GpsLocations");

            migrationBuilder.DropColumn(
                name: "PaceMax",
                table: "GpsSessions");

            migrationBuilder.DropColumn(
                name: "PaceMin",
                table: "GpsSessions");

            migrationBuilder.AddColumn<double>(
                name: "MaxSpeed",
                table: "GpsSessions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinSpeed",
                table: "GpsSessions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
