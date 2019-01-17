using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Upgrade.Infrastructure.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParkId = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ParkIP = table.Column<string>(nullable: true),
                    Params = table.Column<string>(nullable: true),
                    RespMsg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SNumber = table.Column<string>(nullable: true),
                    SName = table.Column<string>(nullable: true),
                    FilePathName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UpgradeItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    IsValid = table.Column<bool>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creater = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpgradeItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientUpgradeItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParkId = table.Column<string>(nullable: true),
                    IsUpgradeSucess = table.Column<bool>(nullable: false),
                    SuccessTime = table.Column<DateTime>(nullable: false),
                    UpgradeItemId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creater = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientUpgradeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientUpgradeItems_UpgradeItems_UpgradeItemId",
                        column: x => x.UpgradeItemId,
                        principalTable: "UpgradeItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpgradeFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    FileOSSUrl = table.Column<string>(nullable: true),
                    UpgradeItemId = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Creater = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpgradeFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpgradeFiles_UpgradeItems_UpgradeItemId",
                        column: x => x.UpgradeItemId,
                        principalTable: "UpgradeItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientUpgradeItems_UpgradeItemId",
                table: "ClientUpgradeItems",
                column: "UpgradeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UpgradeFiles_UpgradeItemId",
                table: "UpgradeFiles",
                column: "UpgradeItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientUpgradeItems");

            migrationBuilder.DropTable(
                name: "RequestLogs");

            migrationBuilder.DropTable(
                name: "Softwares");

            migrationBuilder.DropTable(
                name: "UpgradeFiles");

            migrationBuilder.DropTable(
                name: "UpgradeItems");
        }
    }
}
