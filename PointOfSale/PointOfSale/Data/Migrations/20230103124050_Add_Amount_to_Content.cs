using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Data.Migrations
{
    public partial class Add_Amount_to_Content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Contents",
                newName: "Amount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Contents",
                newName: "Quantity");
        }
    }
}
