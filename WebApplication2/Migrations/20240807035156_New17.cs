using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class New17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedDepartmentName",
                table: "MetadataForms");

            migrationBuilder.DropColumn(
                name: "SelectedDocumentName",
                table: "MetadataForms");

            migrationBuilder.DropColumn(
                name: "SelectedSiteName",
                table: "MetadataForms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SelectedDepartmentName",
                table: "MetadataForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SelectedDocumentName",
                table: "MetadataForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SelectedSiteName",
                table: "MetadataForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
