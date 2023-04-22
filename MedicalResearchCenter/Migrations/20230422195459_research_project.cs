using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalResearchCenter.Migrations
{
    /// <inheritdoc />
    public partial class research_project : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResearchProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientResearchProject",
                columns: table => new
                {
                    PatientsId = table.Column<int>(type: "int", nullable: false),
                    ResearchProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientResearchProject", x => new { x.PatientsId, x.ResearchProjectsId });
                    table.ForeignKey(
                        name: "FK_PatientResearchProject_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientResearchProject_ResearchProjects_ResearchProjectsId",
                        column: x => x.ResearchProjectsId,
                        principalTable: "ResearchProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientResearchProject_ResearchProjectsId",
                table: "PatientResearchProject",
                column: "ResearchProjectsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientResearchProject");

            migrationBuilder.DropTable(
                name: "ResearchProjects");
        }
    }
}
