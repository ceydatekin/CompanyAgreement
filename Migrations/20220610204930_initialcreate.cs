using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyAgreement.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicianLogins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: true),
                    Surname = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: true),
                    Password = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: true),
                    AcademicianDepartment = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicianLogins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminLogins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: true),
                    Password = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminLogins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAuthorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SGKNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAuthorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: true),
                    Surname = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GSM = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyLogin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: true),
                    Password = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: true),
                    Companyid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLogin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicPrivate = table.Column<bool>(type: "bit", nullable: false),
                    MeetingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_CompanyInformation_CompanyInformationId",
                        column: x => x.CompanyInformationId,
                        principalTable: "CompanyInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDepartments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CompanyDepartmentId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDepartments", x => new { x.DepartmentId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_CompanyDepartments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GSM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "Varchar(300)", maxLength: 300, nullable: true),
                    Province = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: true),
                    District = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractInformation_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractSituation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Situation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractSituation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractSituation_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyInformationId",
                table: "Companies",
                column: "CompanyInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDepartments_CompanyId",
                table: "CompanyDepartments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractInformation_CompanyId",
                table: "ContractInformation",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractSituation_CompanyId",
                table: "ContractSituation",
                column: "CompanyId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicianLogins");

            migrationBuilder.DropTable(
                name: "AdminLogins");

            migrationBuilder.DropTable(
                name: "CompanyAuthorities");

            migrationBuilder.DropTable(
                name: "CompanyDepartments");

            migrationBuilder.DropTable(
                name: "CompanyLogin");

            migrationBuilder.DropTable(
                name: "ContractInformation");

            migrationBuilder.DropTable(
                name: "ContractSituation");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyInformation");
        }
    }
}
