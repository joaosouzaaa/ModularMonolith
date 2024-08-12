using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctor.Infrasctructure.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleDoctorAttendant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Doctors_DoctorId",
                schema: "Doctor",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                schema: "Doctor",
                table: "Schedules",
                newName: "DoctorAttendantId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_DoctorId",
                schema: "Doctor",
                table: "Schedules",
                newName: "IX_Schedules_DoctorAttendantId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorAttendant_Schedule",
                schema: "Doctor",
                table: "Schedules",
                column: "DoctorAttendantId",
                principalSchema: "Doctor",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorAttendant_Schedule",
                schema: "Doctor",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "DoctorAttendantId",
                schema: "Doctor",
                table: "Schedules",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_DoctorAttendantId",
                schema: "Doctor",
                table: "Schedules",
                newName: "IX_Schedules_DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Doctors_DoctorId",
                schema: "Doctor",
                table: "Schedules",
                column: "DoctorId",
                principalSchema: "Doctor",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
