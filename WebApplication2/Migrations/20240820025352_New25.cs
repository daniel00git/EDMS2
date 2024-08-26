using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class New25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Departments_SiteId",
                table: "Departments",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Sites_SiteId",
                table: "Departments",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Sites_SiteId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_SiteId",
                table: "Departments");
        }
    }
}
