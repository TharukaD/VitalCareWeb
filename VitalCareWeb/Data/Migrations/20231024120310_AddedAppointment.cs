using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitalCareWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IdentityNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReasonForVisit = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SpecialityId = table.Column<int>(type: "int", nullable: false),
                    SpecialityName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    DoctorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PreferredDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
