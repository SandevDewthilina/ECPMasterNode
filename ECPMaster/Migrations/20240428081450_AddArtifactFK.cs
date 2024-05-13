using Microsoft.EntityFrameworkCore.Migrations;

namespace ECPMaster.Migrations
{
    public partial class AddArtifactFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtifactId",
                table: "EcpDeployments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EcpDeployments_ArtifactId",
                table: "EcpDeployments",
                column: "ArtifactId");

            migrationBuilder.AddForeignKey(
                name: "FK_EcpDeployments_EcpArtifacts_ArtifactId",
                table: "EcpDeployments",
                column: "ArtifactId",
                principalTable: "EcpArtifacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EcpDeployments_EcpArtifacts_ArtifactId",
                table: "EcpDeployments");

            migrationBuilder.DropIndex(
                name: "IX_EcpDeployments_ArtifactId",
                table: "EcpDeployments");

            migrationBuilder.DropColumn(
                name: "ArtifactId",
                table: "EcpDeployments");
        }
    }
}
