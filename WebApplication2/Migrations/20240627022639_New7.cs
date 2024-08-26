using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class New7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentTest",
                table: "MetadataForms",
                newName: "DocumentDescription");

            migrationBuilder.AddColumn<string>(
                name: "DocumentComment",
                table: "MetadataForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentComment",
                table: "MetadataForms");

            migrationBuilder.RenameColumn(
                name: "DocumentDescription",
                table: "MetadataForms",
                newName: "DocumentTest");
        }
    }
}
