using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class New6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MetadataForms",
                newName: "DocumentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DocumentType",
                newName: "DocumentTypeOptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "MetadataForms",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DocumentTypeOptionId",
                table: "DocumentType",
                newName: "Id");
        }
    }
}
