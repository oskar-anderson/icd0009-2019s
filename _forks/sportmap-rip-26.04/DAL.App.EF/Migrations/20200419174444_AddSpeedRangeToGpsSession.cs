using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class AddSpeedRangeToGpsSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MaxSpeed",
                table: "GpsSessions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinSpeed",
                table: "GpsSessions",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxSpeed",
                table: "GpsSessions");

            migrationBuilder.DropColumn(
                name: "MinSpeed",
                table: "GpsSessions");
        }
    }
}
