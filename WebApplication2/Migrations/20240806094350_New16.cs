using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class New16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SiteName",
                table: "MetadataForms",
                newName: "SelectedSiteName");

            migrationBuilder.RenameColumn(
                name: "ParentDocumentName",
                table: "MetadataForms",
                newName: "SelectedDocumentName");

            migrationBuilder.RenameColumn(
                name: "DepartmentName",
                table: "MetadataForms",
                newName: "SelectedDepartmentName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelectedSiteName",
                table: "MetadataForms",
                newName: "SiteName");

            migrationBuilder.RenameColumn(
                name: "SelectedDocumentName",
                table: "MetadataForms",
                newName: "ParentDocumentName");

            migrationBuilder.RenameColumn(
                name: "SelectedDepartmentName",
                table: "MetadataForms",
                newName: "DepartmentName");
        }
    }
}
