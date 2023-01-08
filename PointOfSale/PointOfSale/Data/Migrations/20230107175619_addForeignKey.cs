using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Data.Migrations
{
    public partial class addForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationStocks_CartItems_CartOrder",
                table: "OperationStocks");

            migrationBuilder.DropIndex(
                name: "IX_OperationStocks_CartOrder",
                table: "OperationStocks");

            migrationBuilder.DropColumn(
                name: "CartOrder",
                table: "OperationStocks");

            migrationBuilder.CreateIndex(
                name: "IX_OperationStocks_CartOrderId",
                table: "OperationStocks",
                column: "CartOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationStocks_CartItems_CartOrderId",
                table: "OperationStocks",
                column: "CartOrderId",
                principalTable: "CartItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationStocks_CartItems_CartOrderId",
                table: "OperationStocks");

            migrationBuilder.DropIndex(
                name: "IX_OperationStocks_CartOrderId",
                table: "OperationStocks");

            migrationBuilder.AddColumn<int>(
                name: "CartOrder",
                table: "OperationStocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationStocks_CartOrder",
                table: "OperationStocks",
                column: "CartOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationStocks_CartItems_CartOrder",
                table: "OperationStocks",
                column: "CartOrder",
                principalTable: "CartItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
