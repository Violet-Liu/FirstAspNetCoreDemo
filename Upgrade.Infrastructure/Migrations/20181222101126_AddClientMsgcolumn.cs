using Microsoft.EntityFrameworkCore.Migrations;

namespace Upgrade.Infrastructure.Migrations
{
    public partial class AddClientMsgcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UpgradeRespMsg",
                table: "ClientUpgradeItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpgradeRespMsg",
                table: "ClientUpgradeItems");
        }
    }
}
