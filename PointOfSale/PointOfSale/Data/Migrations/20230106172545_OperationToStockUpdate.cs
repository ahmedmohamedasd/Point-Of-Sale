using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Data.Migrations
{
    public partial class OperationToStockUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationStocks_CartItems_CartOrder",
                table: "OperationStocks");

            migrationBuilder.AlterColumn<int>(
                name: "CartOrder",
                table: "OperationStocks",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CartOrderId",
                table: "OperationStocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationStocks_CartItems_CartOrder",
                table: "OperationStocks",
                column: "CartOrder",
                principalTable: "CartItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationStocks_CartItems_CartOrder",
                table: "OperationStocks");

            migrationBuilder.DropColumn(
                name: "CartOrderId",
                table: "OperationStocks");

            migrationBuilder.AlterColumn<int>(
                name: "CartOrder",
                table: "OperationStocks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationStocks_CartItems_CartOrder",
                table: "OperationStocks",
                column: "CartOrder",
                principalTable: "CartItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
