using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Upgrade.Infrastructure.Migrations
{
    public partial class alter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileOSSUrl",
                table: "UpgradeFiles",
                newName: "BucketName");

            migrationBuilder.RenameColumn(
                name: "Params",
                table: "RequestLogs",
                newName: "ActionName");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UpgradeItems",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SuccessTime",
                table: "ClientUpgradeItems",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UpgradeItems");

            migrationBuilder.RenameColumn(
                name: "BucketName",
                table: "UpgradeFiles",
                newName: "FileOSSUrl");

            migrationBuilder.RenameColumn(
                name: "ActionName",
                table: "RequestLogs",
                newName: "Params");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SuccessTime",
                table: "ClientUpgradeItems",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
