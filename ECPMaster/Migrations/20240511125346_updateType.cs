using Microsoft.EntityFrameworkCore.Migrations;

namespace ECPMaster.Migrations
{
    public partial class updateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ECPLog_DeploymentId",
                table: "ECPLog",
                column: "DeploymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ECPLog_ECPDeployment_DeploymentId",
                table: "ECPLog",
                column: "DeploymentId",
                principalTable: "ECPDeployment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ECPLog_ECPDeployment_DeploymentId",
                table: "ECPLog");

            migrationBuilder.DropIndex(
                name: "IX_ECPLog_DeploymentId",
                table: "ECPLog");
        }
    }
}
