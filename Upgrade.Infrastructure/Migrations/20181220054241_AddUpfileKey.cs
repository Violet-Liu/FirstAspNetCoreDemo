using Microsoft.EntityFrameworkCore.Migrations;

namespace Upgrade.Infrastructure.Migrations
{
    public partial class AddUpfileKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "UpgradeFiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "UpgradeFiles");
        }
    }
}
