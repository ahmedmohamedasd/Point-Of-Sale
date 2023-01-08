using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Data.Migrations
{
    public partial class OperationToStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationStocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentID = table.Column<int>(nullable: false),
                    BarItemId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    CartOrder = table.Column<int>(nullable: false),
                    DateOfOrder = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationStocks_CartItems_CartOrder",
                        column: x => x.CartOrder,
                        principalTable: "CartItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationStocks_CartOrder",
                table: "OperationStocks",
                column: "CartOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationStocks");
        }
    }
}
