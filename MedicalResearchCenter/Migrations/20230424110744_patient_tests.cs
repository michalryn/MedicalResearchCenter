using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalResearchCenter.Migrations
{
    /// <inheritdoc />
    public partial class patient_tests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabReferralLabTests");

            migrationBuilder.CreateTable(
                name: "PatientTests",
                columns: table => new
                {
                    LabReferralId = table.Column<int>(type: "int", nullable: false),
                    LabTestId = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTests", x => new { x.LabReferralId, x.LabTestId });
                    table.ForeignKey(
                        name: "FK_PatientTests_LabReferrals_LabReferralId",
                        column: x => x.LabReferralId,
                        principalTable: "LabReferrals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientTests_LabTests_LabTestId",
                        column: x => x.LabTestId,
                        principalTable: "LabTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientTests_LabTestId",
                table: "PatientTests",
                column: "LabTestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientTests");

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
        }
    }
}
