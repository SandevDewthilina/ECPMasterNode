using Microsoft.EntityFrameworkCore.Migrations;

namespace ECPMaster.Migrations
{
    public partial class removeAgentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ECPDeployment_ECPNode_NodeId",
                table: "ECPDeployment");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "ECPDeployment");

            migrationBuilder.AlterColumn<int>(
                name: "NodeId",
                table: "ECPDeployment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ECPDeployment_ECPNode_NodeId",
                table: "ECPDeployment",
                column: "NodeId",
                principalTable: "ECPNode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ECPDeployment_ECPNode_NodeId",
                table: "ECPDeployment");

            migrationBuilder.AlterColumn<int>(
                name: "NodeId",
                table: "ECPDeployment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "ECPDeployment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ECPDeployment_ECPNode_NodeId",
                table: "ECPDeployment",
                column: "NodeId",
                principalTable: "ECPNode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
