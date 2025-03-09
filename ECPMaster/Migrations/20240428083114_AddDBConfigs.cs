using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECPMaster.Migrations
{
    public partial class AddDBConfigs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OperatingSystem",
                table: "EcpNodes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NodeIdentifier",
                table: "EcpNodes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IPv4",
                table: "EcpNodes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EcpDeployments",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DbConfigId",
                table: "EcpDeployments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DbPassword",
                table: "EcpDeployments",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "DbPort",
                table: "EcpDeployments",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "EcpArtifacts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "EcpArtifacts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EcpArtifacts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EcpDbConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Port = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    DbBackupFileName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcpDbConfigs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EcpDeployments_DbConfigId",
                table: "EcpDeployments",
                column: "DbConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_EcpDeployments_EcpDbConfigs_DbConfigId",
                table: "EcpDeployments",
                column: "DbConfigId",
                principalTable: "EcpDbConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EcpDeployments_EcpDbConfigs_DbConfigId",
                table: "EcpDeployments");

            migrationBuilder.DropTable(
                name: "EcpDbConfigs");

            migrationBuilder.DropIndex(
                name: "IX_EcpDeployments_DbConfigId",
                table: "EcpDeployments");

            migrationBuilder.DropColumn(
                name: "DbConfigId",
                table: "EcpDeployments");

            migrationBuilder.DropColumn(
                name: "DbPassword",
                table: "EcpDeployments");

            migrationBuilder.DropColumn(
                name: "DbPort",
                table: "EcpDeployments");

            migrationBuilder.AlterColumn<string>(
                name: "OperatingSystem",
                table: "EcpNodes",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "NodeIdentifier",
                table: "EcpNodes",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "IPv4",
                table: "EcpNodes",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EcpDeployments",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "EcpArtifacts",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "EcpArtifacts",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EcpArtifacts",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
