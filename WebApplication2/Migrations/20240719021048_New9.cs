using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class New9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocTypeLog",
                columns: table => new
                {
                    DocTypeLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocTypeLogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocTypeLogActivity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocTypeLogEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocTypeOptionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentDocTypeOption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewDocTypeOption = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocTypeLog", x => x.DocTypeLogId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocTypeLog");
        }
    }
}
