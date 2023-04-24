using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalResearchCenter.Migrations
{
    /// <inheritdoc />
    public partial class lab_refferals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabRefferals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Consent = table.Column<bool>(type: "bit", nullable: false),
                    ResearchProjectId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabRefferals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabRefferals_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabRefferals_ResearchProjects_ResearchProjectId",
                        column: x => x.ResearchProjectId,
                        principalTable: "ResearchProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Norm = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabRefferalLabTests",
                columns: table => new
                {
                    LabRefferalId = table.Column<int>(type: "int", nullable: false),
                    LabTestId = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabRefferalLabTests", x => new { x.LabRefferalId, x.LabTestId });
                    table.ForeignKey(
                        name: "FK_LabRefferalLabTests_LabRefferals_LabRefferalId",
                        column: x => x.LabRefferalId,
                        principalTable: "LabRefferals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabRefferalLabTests_LabTests_LabTestId",
                        column: x => x.LabTestId,
                        principalTable: "LabTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabRefferalLabTests_LabTestId",
                table: "LabRefferalLabTests",
                column: "LabTestId");

            migrationBuilder.CreateIndex(
                name: "IX_LabRefferals_PatientId",
                table: "LabRefferals",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LabRefferals_ResearchProjectId",
                table: "LabRefferals",
                column: "ResearchProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabRefferalLabTests");

            migrationBuilder.DropTable(
                name: "LabRefferals");

            migrationBuilder.DropTable(
                name: "LabTests");
        }
    }
}
