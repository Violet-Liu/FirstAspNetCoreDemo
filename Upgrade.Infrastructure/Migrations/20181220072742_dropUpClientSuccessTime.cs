using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Upgrade.Infrastructure.Migrations
{
    public partial class dropUpClientSuccessTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuccessTime",
                table: "ClientUpgradeItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SuccessTime",
                table: "ClientUpgradeItems",
                nullable: true);
        }
    }
}
