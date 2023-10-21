using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitalCareWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedShortLongDescriptionToArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Articles",
                newName: "LongDescription");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Articles",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "LongDescription",
                table: "Articles",
                newName: "Description");
        }
    }
}
