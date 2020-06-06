using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 128, nullable: false),
                    LastName = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LangStrs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LangStrs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GpsLocationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    NameId = table.Column<Guid>(nullable: false),
                    DescriptionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpsLocationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GpsLocationTypes_LangStrs_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "LangStrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GpsLocationTypes_LangStrs_NameId",
                        column: x => x.NameId,
                        principalTable: "LangStrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GpsSessionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    NameId = table.Column<Guid>(nullable: false),
                    DescriptionId = table.Column<Guid>(nullable: false),
                    PaceMin = table.Column<int>(nullable: false),
                    PaceMax = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpsSessionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GpsSessionTypes_LangStrs_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "LangStrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GpsSessionTypes_LangStrs_NameId",
                        column: x => x.NameId,
                        principalTable: "LangStrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LangStrTranslation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    Culture = table.Column<string>(maxLength: 5, nullable: false),
                    Value = table.Column<string>(maxLength: 10240, nullable: false),
                    LangStrId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LangStrTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LangStrTranslation_LangStrs_LangStrId",
                        column: x => x.LangStrId,
                        principalTable: "LangStrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    NameId = table.Column<Guid>(nullable: false),
                    DescriptionId = table.Column<Guid>(nullable: false),
                    MapFile = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tracks_LangStrs_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "LangStrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tracks_LangStrs_NameId",
                        column: x => x.NameId,
                        principalTable: "LangStrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GpsSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Description = table.Column<string>(maxLength: 4096, nullable: false),
                    RecordedAt = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Speed = table.Column<double>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    Climb = table.Column<double>(nullable: false),
                    Descent = table.Column<double>(nullable: false),
                    PaceMin = table.Column<double>(nullable: false),
                    PaceMax = table.Column<double>(nullable: false),
                    GpsSessionTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpsSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GpsSessions_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GpsSessions_GpsSessionTypes_GpsSessionTypeId",
                        column: x => x.GpsSessionTypeId,
                        principalTable: "GpsSessionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Accuracy = table.Column<double>(nullable: false),
                    PassOrder = table.Column<int>(nullable: false),
                    TrackId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackPoints_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrackPoints_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GpsLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChangedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    RecordedAt = table.Column<DateTime>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Accuracy = table.Column<double>(nullable: false),
                    Altitude = table.Column<double>(nullable: false),
                    VerticalAccuracy = table.Column<double>(nullable: false),
                    GpsSessionId = table.Column<Guid>(nullable: false),
                    GpsLocationTypeId = table.Column<Guid>(nullable: false),
                    TrackPointId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpsLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GpsLocations_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GpsLocations_GpsLocationTypes_GpsLocationTypeId",
                        column: x => x.GpsLocationTypeId,
                        principalTable: "GpsLocationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GpsLocations_GpsSessions_GpsSessionId",
                        column: x => x.GpsSessionId,
                        principalTable: "GpsSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GpsLocations_TrackPoints_TrackPointId",
                        column: x => x.TrackPointId,
                        principalTable: "TrackPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GpsLocations_AppUserId",
                table: "GpsLocations",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GpsLocations_CreatedAt",
                table: "GpsLocations",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_GpsLocations_GpsLocationTypeId",
                table: "GpsLocations",
                column: "GpsLocationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GpsLocations_GpsSessionId",
                table: "GpsLocations",
                column: "GpsSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_GpsLocations_RecordedAt",
                table: "GpsLocations",
                column: "RecordedAt");

            migrationBuilder.CreateIndex(
                name: "IX_GpsLocations_TrackPointId",
                table: "GpsLocations",
                column: "TrackPointId");

            migrationBuilder.CreateIndex(
                name: "IX_GpsLocationTypes_DescriptionId",
                table: "GpsLocationTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_GpsLocationTypes_NameId",
                table: "GpsLocationTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_GpsSessions_AppUserId",
                table: "GpsSessions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GpsSessions_CreatedAt",
                table: "GpsSessions",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_GpsSessions_GpsSessionTypeId",
                table: "GpsSessions",
                column: "GpsSessionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GpsSessions_RecordedAt",
                table: "GpsSessions",
                column: "RecordedAt");

            migrationBuilder.CreateIndex(
                name: "IX_GpsSessionTypes_DescriptionId",
                table: "GpsSessionTypes",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_GpsSessionTypes_NameId",
                table: "GpsSessionTypes",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_LangStrTranslation_LangStrId",
                table: "LangStrTranslation",
                column: "LangStrId");

            migrationBuilder.CreateIndex(
                name: "IX_LangStrTranslation_Culture_LangStrId",
                table: "LangStrTranslation",
                columns: new[] { "Culture", "LangStrId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackPoints_AppUserId",
                table: "TrackPoints",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackPoints_TrackId",
                table: "TrackPoints",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_AppUserId",
                table: "Tracks",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_DescriptionId",
                table: "Tracks",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_NameId",
                table: "Tracks",
                column: "NameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GpsLocations");

            migrationBuilder.DropTable(
                name: "LangStrTranslation");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "GpsLocationTypes");

            migrationBuilder.DropTable(
                name: "GpsSessions");

            migrationBuilder.DropTable(
                name: "TrackPoints");

            migrationBuilder.DropTable(
                name: "GpsSessionTypes");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LangStrs");
        }
    }
}
