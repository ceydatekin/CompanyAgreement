using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyAgreement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInformation_Companies_CompanyId",
                table: "CompanyInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractSituation_Companies_CompanyId",
                table: "ContractSituation");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractSituation_CompanyAuthorities_CompanyAuthorityId",
                table: "ContractSituation");

            migrationBuilder.DropIndex(
                name: "IX_ContractSituation_CompanyAuthorityId",
                table: "ContractSituation");

            migrationBuilder.DropIndex(
                name: "IX_ContractSituation_CompanyId",
                table: "ContractSituation");

            migrationBuilder.DropIndex(
                name: "IX_CompanyInformation_CompanyId",
                table: "CompanyInformation");

            migrationBuilder.DropColumn(
                name: "CompanyAuthorityId",
                table: "ContractSituation");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ContractSituation");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CompanyInformation");

            migrationBuilder.AddColumn<int>(
                name: "CompanyInformationId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyInformationId",
                table: "Companies",
                column: "CompanyInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanyInformation_CompanyInformationId",
                table: "Companies",
                column: "CompanyInformationId",
                principalTable: "CompanyInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanyInformation_CompanyInformationId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CompanyInformationId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyInformationId",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "CompanyAuthorityId",
                table: "ContractSituation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "ContractSituation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "CompanyInformation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContractSituation_CompanyAuthorityId",
                table: "ContractSituation",
                column: "CompanyAuthorityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractSituation_CompanyId",
                table: "ContractSituation",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInformation_CompanyId",
                table: "CompanyInformation",
                column: "CompanyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInformation_Companies_CompanyId",
                table: "CompanyInformation",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractSituation_Companies_CompanyId",
                table: "ContractSituation",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractSituation_CompanyAuthorities_CompanyAuthorityId",
                table: "ContractSituation",
                column: "CompanyAuthorityId",
                principalTable: "CompanyAuthorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
