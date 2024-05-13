using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECPMaster.Migrations
{
    public partial class renameTableAgents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EcpDeployments_EcpAgents_AgentId",
                table: "EcpDeployments");

            migrationBuilder.DropTable(
                name: "EcpAgents");

            migrationBuilder.DropIndex(
                name: "IX_EcpDeployments_AgentId",
                table: "EcpDeployments");

            migrationBuilder.AddColumn<int>(
                name: "NodeId",
                table: "EcpDeployments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EcpNodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NodeIdentifier = table.Column<string>(nullable: true),
                    NodeState = table.Column<int>(nullable: false),
                    OperatingSystem = table.Column<string>(nullable: true),
                    IPv4 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcpNodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EcpDeployments_NodeId",
                table: "EcpDeployments",
                column: "NodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EcpDeployments_EcpNodes_NodeId",
                table: "EcpDeployments",
                column: "NodeId",
                principalTable: "EcpNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EcpDeployments_EcpNodes_NodeId",
                table: "EcpDeployments");

            migrationBuilder.DropTable(
                name: "EcpNodes");

            migrationBuilder.DropIndex(
                name: "IX_EcpDeployments_NodeId",
                table: "EcpDeployments");

            migrationBuilder.DropColumn(
                name: "NodeId",
                table: "EcpDeployments");

            migrationBuilder.CreateTable(
                name: "EcpAgents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AgentState = table.Column<int>(type: "int", nullable: false),
                    IPv4 = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    NodeIdentifier = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    OperatingSystem = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcpAgents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EcpDeployments_AgentId",
                table: "EcpDeployments",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EcpDeployments_EcpAgents_AgentId",
                table: "EcpDeployments",
                column: "AgentId",
                principalTable: "EcpAgents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
