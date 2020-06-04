using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Base.App.EF.Migrations
{
    public partial class InitialDbCreation9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartMeals_PizzaUsers_PizzaUserId",
                table: "CartMeals");

            migrationBuilder.DropTable(
                name: "ComponentPizzaUsers");

            migrationBuilder.DropTable(
                name: "PizzaUsers");

            migrationBuilder.DropIndex(
                name: "IX_CartMeals_PizzaUserId",
                table: "CartMeals");

            migrationBuilder.DropColumn(
                name: "PizzaUserId",
                table: "CartMeals");

            migrationBuilder.RenameColumn(
                name: "Gross",
                table: "CartMeals",
                newName: "PizzaGross");

            migrationBuilder.AlterColumn<Guid>(
                name: "PizzaId",
                table: "CartMeals",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Changes",
                table: "CartMeals",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ComponentsGross",
                table: "CartMeals",
                type: "decimal(18, 2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Changes",
                table: "CartMeals");

            migrationBuilder.DropColumn(
                name: "ComponentsGross",
                table: "CartMeals");

            migrationBuilder.RenameColumn(
                name: "PizzaGross",
                table: "CartMeals",
                newName: "Gross");

            migrationBuilder.AlterColumn<Guid>(
                name: "PizzaId",
                table: "CartMeals",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "PizzaUserId",
                table: "CartMeals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PizzaUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Changes = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PizzaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzaUsers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PizzaUsers_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComponentPizzaUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PizzaUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentPizzaUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentPizzaUsers_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComponentPizzaUsers_PizzaUsers_PizzaUserId",
                        column: x => x.PizzaUserId,
                        principalTable: "PizzaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartMeals_PizzaUserId",
                table: "CartMeals",
                column: "PizzaUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentPizzaUsers_ComponentId",
                table: "ComponentPizzaUsers",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentPizzaUsers_PizzaUserId",
                table: "ComponentPizzaUsers",
                column: "PizzaUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaUsers_AppUserId",
                table: "PizzaUsers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaUsers_PizzaId",
                table: "PizzaUsers",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartMeals_PizzaUsers_PizzaUserId",
                table: "CartMeals",
                column: "PizzaUserId",
                principalTable: "PizzaUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
