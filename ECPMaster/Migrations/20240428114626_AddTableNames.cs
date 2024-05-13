using Microsoft.EntityFrameworkCore.Migrations;

namespace ECPMaster.Migrations
{
    public partial class AddTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EcpDeployments_EcpArtifacts_ArtifactId",
                table: "EcpDeployments");

            migrationBuilder.DropForeignKey(
                name: "FK_EcpDeployments_EcpDbConfigs_DbConfigId",
                table: "EcpDeployments");

            migrationBuilder.DropForeignKey(
                name: "FK_EcpDeployments_EcpNodes_NodeId",
                table: "EcpDeployments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EcpNodes",
                table: "EcpNodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EcpDeployments",
                table: "EcpDeployments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EcpDbConfigs",
                table: "EcpDbConfigs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EcpArtifacts",
                table: "EcpArtifacts");

            migrationBuilder.RenameTable(
                name: "EcpNodes",
                newName: "ECPNode");

            migrationBuilder.RenameTable(
                name: "EcpDeployments",
                newName: "ECPDeployment");

            migrationBuilder.RenameTable(
                name: "EcpDbConfigs",
                newName: "ECPDbConfig");

            migrationBuilder.RenameTable(
                name: "EcpArtifacts",
                newName: "ECPArtifact");

            migrationBuilder.RenameIndex(
                name: "IX_EcpDeployments_NodeId",
                table: "ECPDeployment",
                newName: "IX_ECPDeployment_NodeId");

            migrationBuilder.RenameIndex(
                name: "IX_EcpDeployments_DbConfigId",
                table: "ECPDeployment",
                newName: "IX_ECPDeployment_DbConfigId");

            migrationBuilder.RenameIndex(
                name: "IX_EcpDeployments_ArtifactId",
                table: "ECPDeployment",
                newName: "IX_ECPDeployment_ArtifactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ECPNode",
                table: "ECPNode",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ECPDeployment",
                table: "ECPDeployment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ECPDbConfig",
                table: "ECPDbConfig",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ECPArtifact",
                table: "ECPArtifact",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ECPDeployment_ECPArtifact_ArtifactId",
                table: "ECPDeployment",
                column: "ArtifactId",
                principalTable: "ECPArtifact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ECPDeployment_ECPDbConfig_DbConfigId",
                table: "ECPDeployment",
                column: "DbConfigId",
                principalTable: "ECPDbConfig",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ECPDeployment_ECPNode_NodeId",
                table: "ECPDeployment",
                column: "NodeId",
                principalTable: "ECPNode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ECPDeployment_ECPArtifact_ArtifactId",
                table: "ECPDeployment");

            migrationBuilder.DropForeignKey(
                name: "FK_ECPDeployment_ECPDbConfig_DbConfigId",
                table: "ECPDeployment");

            migrationBuilder.DropForeignKey(
                name: "FK_ECPDeployment_ECPNode_NodeId",
                table: "ECPDeployment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ECPNode",
                table: "ECPNode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ECPDeployment",
                table: "ECPDeployment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ECPDbConfig",
                table: "ECPDbConfig");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ECPArtifact",
                table: "ECPArtifact");

            migrationBuilder.RenameTable(
                name: "ECPNode",
                newName: "EcpNodes");

            migrationBuilder.RenameTable(
                name: "ECPDeployment",
                newName: "EcpDeployments");

            migrationBuilder.RenameTable(
                name: "ECPDbConfig",
                newName: "EcpDbConfigs");

            migrationBuilder.RenameTable(
                name: "ECPArtifact",
                newName: "EcpArtifacts");

            migrationBuilder.RenameIndex(
                name: "IX_ECPDeployment_NodeId",
                table: "EcpDeployments",
                newName: "IX_EcpDeployments_NodeId");

            migrationBuilder.RenameIndex(
                name: "IX_ECPDeployment_DbConfigId",
                table: "EcpDeployments",
                newName: "IX_EcpDeployments_DbConfigId");

            migrationBuilder.RenameIndex(
                name: "IX_ECPDeployment_ArtifactId",
                table: "EcpDeployments",
                newName: "IX_EcpDeployments_ArtifactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EcpNodes",
                table: "EcpNodes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EcpDeployments",
                table: "EcpDeployments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EcpDbConfigs",
                table: "EcpDbConfigs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EcpArtifacts",
                table: "EcpArtifacts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EcpDeployments_EcpArtifacts_ArtifactId",
                table: "EcpDeployments",
                column: "ArtifactId",
                principalTable: "EcpArtifacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EcpDeployments_EcpDbConfigs_DbConfigId",
                table: "EcpDeployments",
                column: "DbConfigId",
                principalTable: "EcpDbConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EcpDeployments_EcpNodes_NodeId",
                table: "EcpDeployments",
                column: "NodeId",
                principalTable: "EcpNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
