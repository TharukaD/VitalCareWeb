using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitalCareWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPriorityToAppointmentReason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "AppointmentReasons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "AppointmentReasons");
        }
    }
}
