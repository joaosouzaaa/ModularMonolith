using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Appointment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Appointment");

            migrationBuilder.CreateTable(
                name: "AppointmentsTime",
                schema: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    doctor_attendant_id = table.Column<int>(type: "integer", nullable: false),
                    patient_client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsTime", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentsTime",
                schema: "Appointment");
        }
    }
}
