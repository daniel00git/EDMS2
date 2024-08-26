using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentFile",
                table: "MetadataForms",
                newName: "DocumentTest");

            migrationBuilder.AddColumn<byte[]>(
                name: "DocumentContent",
                table: "MetadataForms",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentContent",
                table: "MetadataForms");

            migrationBuilder.RenameColumn(
                name: "DocumentTest",
                table: "MetadataForms",
                newName: "DocumentFile");
        }
    }
}
