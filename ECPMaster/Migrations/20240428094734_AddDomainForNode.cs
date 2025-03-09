using Microsoft.EntityFrameworkCore.Migrations;

namespace ECPMaster.Migrations
{
    public partial class AddDomainForNode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domain",
                table: "EcpDeployments");

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "EcpNodes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubDomainPrefix",
                table: "EcpDeployments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domain",
                table: "EcpNodes");

            migrationBuilder.DropColumn(
                name: "SubDomainPrefix",
                table: "EcpDeployments");

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "EcpDeployments",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
