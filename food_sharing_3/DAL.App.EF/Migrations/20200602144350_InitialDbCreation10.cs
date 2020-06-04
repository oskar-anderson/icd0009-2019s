using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Base.App.EF.Migrations
{
    public partial class InitialDbCreation10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadyBy",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Carts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReadyBy",
                table: "Carts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
