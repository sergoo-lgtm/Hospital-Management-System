using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HospitalManagementSystemAPIVersion.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Name", "Specialization" },
                values: new object[,]
                {
                    { 1, "Dr. Ahmed Hossam", "Cardiology" },
                    { 2, "Dr. Sara Khaled", "Dermatology" },
                    { 3, "Dr. Mohamed Adel", "Neurology" },
                    { 4, "Dr. Laila Samir", "Pediatrics" },
                    { 5, "Dr. Omar Fathy", "Orthopedics" },
                    { 6, "Dr. Nour Hassan", "General Surgery" },
                    { 7, "Dr. Hana Tamer", "Gynecology" },
                    { 8, "Dr. Youssef Mahmoud", "Ophthalmology" },
                    { 9, "Dr. Mona Kamal", "ENT" },
                    { 10, "Dr. Khaled Ali", "Psychiatry" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Ahmed Ali", "01012345678" },
                    { 2, "Sara Hassan", "01023456789" },
                    { 3, "Mohamed Tamer", "01034567890" },
                    { 4, "Laila Fathy", "01045678901" },
                    { 5, "Omar Mostafa", "01056789012" },
                    { 6, "Nour El-Din", "01067890123" },
                    { 7, "Hana Mahmoud", "01078901234" },
                    { 8, "Youssef Samir", "01089012345" },
                    { 9, "Mona Kamal", "01090123456" },
                    { 10, "Khaled Adel", "01001234567" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Date", "DoctorId", "PatientId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 24, 0, 0, 0, 0, DateTimeKind.Local), 1, 1, "Scheduled" },
                    { 2, new DateTime(2026, 3, 25, 0, 0, 0, 0, DateTimeKind.Local), 2, 2, "Scheduled" },
                    { 3, new DateTime(2026, 3, 26, 0, 0, 0, 0, DateTimeKind.Local), 3, 3, "Scheduled" },
                    { 4, new DateTime(2026, 3, 27, 0, 0, 0, 0, DateTimeKind.Local), 4, 4, "Scheduled" },
                    { 5, new DateTime(2026, 3, 28, 0, 0, 0, 0, DateTimeKind.Local), 5, 5, "Scheduled" },
                    { 6, new DateTime(2026, 3, 29, 0, 0, 0, 0, DateTimeKind.Local), 6, 6, "Scheduled" },
                    { 7, new DateTime(2026, 3, 30, 0, 0, 0, 0, DateTimeKind.Local), 7, 7, "Scheduled" },
                    { 8, new DateTime(2026, 3, 31, 0, 0, 0, 0, DateTimeKind.Local), 8, 8, "Scheduled" },
                    { 9, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Local), 9, 9, "Scheduled" },
                    { 10, new DateTime(2026, 4, 2, 0, 0, 0, 0, DateTimeKind.Local), 10, 10, "Scheduled" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "AppointmentId", "IsPaid" },
                values: new object[,]
                {
                    { 1, 110, 1, false },
                    { 2, 120, 2, false },
                    { 3, 130, 3, false },
                    { 4, 140, 4, false },
                    { 5, 150, 5, false },
                    { 6, 160, 6, false },
                    { 7, 170, 7, false },
                    { 8, 180, 8, false },
                    { 9, 190, 9, false },
                    { 10, 200, 10, false }
                });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "Id", "AppointmentId", "Medications", "Notes" },
                values: new object[,]
                {
                    { 1, 1, "Paracetamol 500mg", "Patient has mild fever" },
                    { 2, 2, "Cough Syrup", "Patient has cough" },
                    { 3, 3, "Ibuprofen", "Patient has headache" },
                    { 4, 4, "Vitamin C", "Patient has flu" },
                    { 5, 5, "Antihistamine", "Patient has cold" },
                    { 6, 6, "Muscle Relaxant", "Patient has back pain" },
                    { 7, 7, "Antacid", "Patient has stomach ache" },
                    { 8, 8, "Antihistamine", "Patient has allergies" },
                    { 9, 9, "Painkiller", "Patient has migraine" },
                    { 10, 10, "Antibiotic", "Patient has infection" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AppointmentId",
                table: "Payments",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_AppointmentId",
                table: "Prescriptions",
                column: "AppointmentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
