using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class New24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Sites_SiteDepartmentNameSiteId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_SiteDepartmentNameSiteId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "SiteDepartmentId",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "SiteDepartmentNameSiteId",
                table: "Departments",
                newName: "SiteId");

            migrationBuilder.AddColumn<string>(
                name: "SiteName",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiteName",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "SiteId",
                table: "Departments",
                newName: "SiteDepartmentNameSiteId");

            migrationBuilder.AddColumn<int>(
                name: "SiteDepartmentId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_SiteDepartmentNameSiteId",
                table: "Departments",
                column: "SiteDepartmentNameSiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Sites_SiteDepartmentNameSiteId",
                table: "Departments",
                column: "SiteDepartmentNameSiteId",
                principalTable: "Sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
