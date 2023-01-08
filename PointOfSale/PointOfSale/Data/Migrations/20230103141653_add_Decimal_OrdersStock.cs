using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Data.Migrations
{
    public partial class add_Decimal_OrdersStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "OrdersStocks",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "OrdersStocks",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");
        }
    }
}
