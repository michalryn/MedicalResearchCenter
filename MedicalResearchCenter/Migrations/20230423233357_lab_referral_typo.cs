using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalResearchCenter.Migrations
{
    /// <inheritdoc />
    public partial class lab_referral_typo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabRefferalLabTests");

            migrationBuilder.DropTable(
                name: "LabRefferals");

            migrationBuilder.CreateTable(
                name: "LabReferrals",
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
                    table.PrimaryKey("PK_LabReferrals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabReferrals_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabReferrals_ResearchProjects_ResearchProjectId",
                        column: x => x.ResearchProjectId,
                        principalTable: "ResearchProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabReferralLabTests",
                columns: table => new
                {
                    LabReferralId = table.Column<int>(type: "int", nullable: false),
                    LabTestId = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabReferralLabTests", x => new { x.LabReferralId, x.LabTestId });
                    table.ForeignKey(
                        name: "FK_LabReferralLabTests_LabReferrals_LabReferralId",
                        column: x => x.LabReferralId,
                        principalTable: "LabReferrals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabReferralLabTests_LabTests_LabTestId",
                        column: x => x.LabTestId,
                        principalTable: "LabTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabReferralLabTests_LabTestId",
                table: "LabReferralLabTests",
                column: "LabTestId");

            migrationBuilder.CreateIndex(
                name: "IX_LabReferrals_PatientId",
                table: "LabReferrals",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LabReferrals_ResearchProjectId",
                table: "LabReferrals",
                column: "ResearchProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabReferralLabTests");

            migrationBuilder.DropTable(
                name: "LabReferrals");

            migrationBuilder.CreateTable(
                name: "LabRefferals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ResearchProjectId = table.Column<int>(type: "int", nullable: false),
                    Consent = table.Column<bool>(type: "bit", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
    }
}
