using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalResearchCenter.Migrations
{
    /// <inheritdoc />
    public partial class labTest_details_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Norm",
                table: "LabTests",
                newName: "NormTo");

            migrationBuilder.AddColumn<double>(
                name: "NormFrom",
                table: "LabTests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormFrom",
                table: "LabTests");

            migrationBuilder.RenameColumn(
                name: "NormTo",
                table: "LabTests",
                newName: "Norm");
        }
    }
}
