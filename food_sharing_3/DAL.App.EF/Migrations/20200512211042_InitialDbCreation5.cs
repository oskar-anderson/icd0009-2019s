using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Base.App.EF.Migrations
{
    public partial class InitialDbCreation5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartMeals_PizzaFinals_PizzaFinalId",
                table: "CartMeals");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_InvoiceLines_InvoiceLineId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Categories_CategoryId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Sizes_SizeId",
                table: "Pizzas");

            migrationBuilder.DropForeignKey(
                name: "FK_PizzaTemplates_Categories_CategoryId",
                table: "PizzaTemplates");

            migrationBuilder.DropTable(
                name: "ComponentPrices");

            migrationBuilder.DropTable(
                name: "InvoiceLines");

            migrationBuilder.DropTable(
                name: "PizzaComponents");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "PizzaFinals");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Pizzas_SizeId",
                table: "Pizzas");

            migrationBuilder.DropIndex(
                name: "IX_Items_InvoiceLineId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_CartMeals_PizzaFinalId",
                table: "CartMeals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RestaurantFoods");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "RestaurantFoods");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "InvoiceLineId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Net",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PizzaFinalId",
                table: "CartMeals");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categorys");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Carts",
                newName: "Gross");

            migrationBuilder.AddColumn<string>(
                name: "SizeName",
                table: "Pizzas",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SizeNumber",
                table: "Pizzas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Gross",
                table: "Components",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<bool>(
                name: "AsDelivery",
                table: "Carts",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Carts",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Carts",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Carts",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Carts",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Carts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Gross",
                table: "CartMeals",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CartMeals",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PizzaUserId",
                table: "CartMeals",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ForMeal",
                table: "Categorys",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ForPizzaTemplate",
                table: "Categorys",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorys",
                table: "Categorys",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ComponentPizzaTPLs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    ComponentId = table.Column<Guid>(nullable: false),
                    PizzaTemplateId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentPizzaTPLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentPizzaTPLs_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComponentPizzaTPLs_PizzaTemplates_PizzaTemplateId",
                        column: x => x.PizzaTemplateId,
                        principalTable: "PizzaTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PizzaUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    PizzaId = table.Column<Guid>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    Changes = table.Column<string>(maxLength: 256, nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    ComponentId = table.Column<Guid>(nullable: false),
                    PizzaUserId = table.Column<Guid>(nullable: false)
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
                name: "IX_ComponentPizzaTPLs_ComponentId",
                table: "ComponentPizzaTPLs",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentPizzaTPLs_PizzaTemplateId",
                table: "ComponentPizzaTPLs",
                column: "PizzaTemplateId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Categorys_CategoryId",
                table: "Meals",
                column: "CategoryId",
                principalTable: "Categorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PizzaTemplates_Categorys_CategoryId",
                table: "PizzaTemplates",
                column: "CategoryId",
                principalTable: "Categorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartMeals_PizzaUsers_PizzaUserId",
                table: "CartMeals");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Categorys_CategoryId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_PizzaTemplates_Categorys_CategoryId",
                table: "PizzaTemplates");

            migrationBuilder.DropTable(
                name: "ComponentPizzaTPLs");

            migrationBuilder.DropTable(
                name: "ComponentPizzaUsers");

            migrationBuilder.DropTable(
                name: "PizzaUsers");

            migrationBuilder.DropIndex(
                name: "IX_CartMeals_PizzaUserId",
                table: "CartMeals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorys",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "SizeName",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "SizeNumber",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "Gross",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Gross",
                table: "CartMeals");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CartMeals");

            migrationBuilder.DropColumn(
                name: "PizzaUserId",
                table: "CartMeals");

            migrationBuilder.DropColumn(
                name: "ForMeal",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "ForPizzaTemplate",
                table: "Categorys");

            migrationBuilder.RenameTable(
                name: "Categorys",
                newName: "Categories");

            migrationBuilder.RenameColumn(
                name: "Gross",
                table: "Carts",
                newName: "Total");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RestaurantFoods",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                table: "RestaurantFoods",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "SizeId",
                table: "Pizzas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceLineId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "Net",
                table: "Items",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                table: "Items",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<bool>(
                name: "AsDelivery",
                table: "Carts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PizzaFinalId",
                table: "CartMeals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ComponentPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Gross = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Since = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Until = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentPrices_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComponentPrices_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Since = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Until = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    NationalIdentificationNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Since = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThisIsMe = table.Column<bool>(type: "bit", nullable: false),
                    Until = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PizzaFinals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Changes = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Gross = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PizzaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaFinals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzaFinals_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalGross = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TotalNet = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PizzaComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PizzaFinalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PizzaTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzaComponents_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PizzaComponents_PizzaFinals_PizzaFinalId",
                        column: x => x.PizzaFinalId,
                        principalTable: "PizzaFinals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PizzaComponents_PizzaTemplates_PizzaTemplateId",
                        column: x => x.PizzaTemplateId,
                        principalTable: "PizzaTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Gross = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Net = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_SizeId",
                table: "Pizzas",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_InvoiceLineId",
                table: "Items",
                column: "InvoiceLineId");

            migrationBuilder.CreateIndex(
                name: "IX_CartMeals_PizzaFinalId",
                table: "CartMeals",
                column: "PizzaFinalId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentPrices_ComponentId",
                table: "ComponentPrices",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentPrices_RestaurantId",
                table: "ComponentPrices",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_CartId",
                table: "InvoiceLines",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_InvoiceId",
                table: "InvoiceLines",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentMethodId",
                table: "Invoices",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PersonId",
                table: "Invoices",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_RestaurantId",
                table: "Invoices",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_AppUserId",
                table: "Persons",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaComponents_ComponentId",
                table: "PizzaComponents",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaComponents_PizzaFinalId",
                table: "PizzaComponents",
                column: "PizzaFinalId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaComponents_PizzaTemplateId",
                table: "PizzaComponents",
                column: "PizzaTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaFinals_PizzaId",
                table: "PizzaFinals",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartMeals_PizzaFinals_PizzaFinalId",
                table: "CartMeals",
                column: "PizzaFinalId",
                principalTable: "PizzaFinals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_InvoiceLines_InvoiceLineId",
                table: "Items",
                column: "InvoiceLineId",
                principalTable: "InvoiceLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Categories_CategoryId",
                table: "Meals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Sizes_SizeId",
                table: "Pizzas",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PizzaTemplates_Categories_CategoryId",
                table: "PizzaTemplates",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
