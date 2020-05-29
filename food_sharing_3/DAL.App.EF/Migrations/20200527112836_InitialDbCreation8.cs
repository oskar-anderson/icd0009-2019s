using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Base.App.EF.Migrations
{
    public partial class InitialDbCreation8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartMeals_PizzaTemplates_PizzaTemplateId",
                table: "CartMeals");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantFoods_PizzaTemplates_PizzaTemplateId",
                table: "RestaurantFoods");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantFoods_PizzaTemplateId",
                table: "RestaurantFoods");

            migrationBuilder.DropIndex(
                name: "IX_CartMeals_PizzaTemplateId",
                table: "CartMeals");

            migrationBuilder.DropColumn(
                name: "PizzaTemplateId",
                table: "RestaurantFoods");

            migrationBuilder.DropColumn(
                name: "PizzaTemplateId",
                table: "CartMeals");

            migrationBuilder.AlterColumn<Guid>(
                name: "PizzaId",
                table: "RestaurantFoods",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PizzaId",
                table: "RestaurantFoods",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "PizzaTemplateId",
                table: "RestaurantFoods",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PizzaTemplateId",
                table: "CartMeals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantFoods_PizzaTemplateId",
                table: "RestaurantFoods",
                column: "PizzaTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CartMeals_PizzaTemplateId",
                table: "CartMeals",
                column: "PizzaTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartMeals_PizzaTemplates_PizzaTemplateId",
                table: "CartMeals",
                column: "PizzaTemplateId",
                principalTable: "PizzaTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantFoods_PizzaTemplates_PizzaTemplateId",
                table: "RestaurantFoods",
                column: "PizzaTemplateId",
                principalTable: "PizzaTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
