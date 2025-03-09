using Microsoft.EntityFrameworkCore.Migrations;

namespace ECPMaster.Migrations
{
    public partial class CleanDeployENt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DbPassword",
                table: "ECPDeployment");

            migrationBuilder.DropColumn(
                name: "DbPort",
                table: "ECPDeployment");

            migrationBuilder.DropColumn(
                name: "SubDomainPrefix",
                table: "ECPDeployment");

            migrationBuilder.AddColumn<string>(
                name: "SubDomain",
                table: "ECPDeployment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubDomain",
                table: "ECPDeployment");

            migrationBuilder.AddColumn<string>(
                name: "DbPassword",
                table: "ECPDeployment",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DbPort",
                table: "ECPDeployment",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubDomainPrefix",
                table: "ECPDeployment",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
