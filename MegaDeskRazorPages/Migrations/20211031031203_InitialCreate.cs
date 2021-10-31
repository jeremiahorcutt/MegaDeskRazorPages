using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MegaDeskRazorPages.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Desk",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    width = table.Column<int>(type: "int", nullable: false),
                    depth = table.Column<int>(type: "int", nullable: false),
                    drawers = table.Column<int>(type: "int", nullable: false),
                    Material = table.Column<int>(type: "int", nullable: false),
                    area = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desk", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DeskQuote",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeskID = table.Column<int>(type: "int", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuoteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuoteTotal = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    NumberOfDrawers = table.Column<int>(type: "int", nullable: false),
                    DeskMaterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RushDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeskQuote", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DeskQuote_Desk_DeskID",
                        column: x => x.DeskID,
                        principalTable: "Desk",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeskQuote_DeskID",
                table: "DeskQuote",
                column: "DeskID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeskQuote");

            migrationBuilder.DropTable(
                name: "Desk");
        }
    }
}
