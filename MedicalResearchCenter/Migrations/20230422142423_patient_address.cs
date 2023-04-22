using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalResearchCenter.Migrations
{
    /// <inheritdoc />
    public partial class patient_address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Patients",
                newName: "UnitNumber");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "UnitNumber",
                table: "Patients",
                newName: "Address");
        }
    }
}
