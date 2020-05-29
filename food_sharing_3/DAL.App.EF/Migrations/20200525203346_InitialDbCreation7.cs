using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Base.App.EF.Migrations
{
    public partial class InitialDbCreation7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartMeals_Meals_MealId",
                table: "CartMeals");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantFoods_Meals_MealId",
                table: "RestaurantFoods");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantFoods_MealId",
                table: "RestaurantFoods");

            migrationBuilder.DropIndex(
                name: "IX_CartMeals_MealId",
                table: "CartMeals");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "RestaurantFoods");

            migrationBuilder.DropColumn(
                name: "Since",
                table: "RestaurantFoods");

            migrationBuilder.DropColumn(
                name: "Until",
                table: "RestaurantFoods");

            migrationBuilder.DropColumn(
                name: "ForMeal",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "ForPizzaTemplate",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "CartMeals");

            migrationBuilder.AddColumn<Guid>(
                name: "PizzaTemplateId",
                table: "RestaurantFoods",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Modifications",
                table: "PizzaTemplates",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Extras",
                table: "PizzaTemplates",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "VarietyState",
                table: "PizzaTemplates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PizzaId",
                table: "CartMeals",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PizzaTemplateId",
                table: "CartMeals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantFoods_PizzaTemplateId",
                table: "RestaurantFoods",
                column: "PizzaTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CartMeals_PizzaId",
                table: "CartMeals",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_CartMeals_PizzaTemplateId",
                table: "CartMeals",
                column: "PizzaTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartMeals_Pizzas_PizzaId",
                table: "CartMeals",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartMeals_Pizzas_PizzaId",
                table: "CartMeals");

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
                name: "IX_CartMeals_PizzaId",
                table: "CartMeals");

            migrationBuilder.DropIndex(
                name: "IX_CartMeals_PizzaTemplateId",
                table: "CartMeals");

            migrationBuilder.DropColumn(
                name: "PizzaTemplateId",
                table: "RestaurantFoods");

            migrationBuilder.DropColumn(
                name: "VarietyState",
                table: "PizzaTemplates");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "CartMeals");

            migrationBuilder.DropColumn(
                name: "PizzaTemplateId",
                table: "CartMeals");

            migrationBuilder.AddColumn<Guid>(
                name: "MealId",
                table: "RestaurantFoods",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Since",
                table: "RestaurantFoods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Until",
                table: "RestaurantFoods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Modifications",
                table: "PizzaTemplates",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Extras",
                table: "PizzaTemplates",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ForMeal",
                table: "Categorys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ForPizzaTemplate",
                table: "Categorys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "MealId",
                table: "CartMeals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantFoods_MealId",
                table: "RestaurantFoods",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_CartMeals_MealId",
                table: "CartMeals",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_CategoryId",
                table: "Meals",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartMeals_Meals_MealId",
                table: "CartMeals",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantFoods_Meals_MealId",
                table: "RestaurantFoods",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
