using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitalCareWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLocationToService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_LocationId",
                table: "Services",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Locations_LocationId",
                table: "Services",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Locations_LocationId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_LocationId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Services");
        }
    }
}
