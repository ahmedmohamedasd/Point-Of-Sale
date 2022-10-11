using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Data.Migrations
{
    public partial class ExpiredTableforce : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpiredStocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarItemId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    DateOfOrder = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpiredStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpiredStocks_BarItems_BarItemId",
                        column: x => x.BarItemId,
                        principalTable: "BarItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpiredStocks_BarItemId",
                table: "ExpiredStocks",
                column: "BarItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpiredStocks");
        }
    }
}
