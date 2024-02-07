using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Patient.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContactInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                schema: "Patient",
                table: "Patients",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                schema: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    phone_number = table.Column<string>(type: "char(11)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    PatientClientId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInfo_PatientClient",
                        column: x => x.PatientClientId,
                        principalSchema: "Patient",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfos_PatientClientId",
                schema: "Patient",
                table: "ContactInfos",
                column: "PatientClientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInfos",
                schema: "Patient");

            migrationBuilder.DropColumn(
                name: "address",
                schema: "Patient",
                table: "Patients");
        }
    }
}
