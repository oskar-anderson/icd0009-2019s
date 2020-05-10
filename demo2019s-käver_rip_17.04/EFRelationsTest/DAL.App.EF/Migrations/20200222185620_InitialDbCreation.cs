using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChildOnes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildOnes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryTwos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryTwos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryOnes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 128, nullable: false),
                    ChildOneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryOnes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrimaryOnes_ChildOnes_ChildOneId",
                        column: x => x.ChildOneId,
                        principalTable: "ChildOnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChildTwos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 128, nullable: false),
                    PrimaryTwoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildTwos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildTwos_PrimaryTwos_PrimaryTwoId",
                        column: x => x.PrimaryTwoId,
                        principalTable: "PrimaryTwos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryThrees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 128, nullable: false),
                    ChildThreeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryThrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChildThrees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(maxLength: 128, nullable: false),
                    PrimaryThreeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildThrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildThrees_PrimaryThrees_PrimaryThreeId",
                        column: x => x.PrimaryThreeId,
                        principalTable: "PrimaryThrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildThrees_PrimaryThreeId",
                table: "ChildThrees",
                column: "PrimaryThreeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildTwos_PrimaryTwoId",
                table: "ChildTwos",
                column: "PrimaryTwoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryOnes_ChildOneId",
                table: "PrimaryOnes",
                column: "ChildOneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryThrees_ChildThreeId",
                table: "PrimaryThrees",
                column: "ChildThreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrimaryThrees_ChildThrees_ChildThreeId",
                table: "PrimaryThrees",
                column: "ChildThreeId",
                principalTable: "ChildThrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildThrees_PrimaryThrees_PrimaryThreeId",
                table: "ChildThrees");

            migrationBuilder.DropTable(
                name: "ChildTwos");

            migrationBuilder.DropTable(
                name: "PrimaryOnes");

            migrationBuilder.DropTable(
                name: "PrimaryTwos");

            migrationBuilder.DropTable(
                name: "ChildOnes");

            migrationBuilder.DropTable(
                name: "PrimaryThrees");

            migrationBuilder.DropTable(
                name: "ChildThrees");
        }
    }
}
