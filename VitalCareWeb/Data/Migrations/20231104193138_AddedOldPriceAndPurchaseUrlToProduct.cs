using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitalCareWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedOldPriceAndPurchaseUrlToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OldPrice",
                table: "Products",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchaseUrl",
                table: "Products");
        }
    }
}
