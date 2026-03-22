using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class ThirdCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { 1, new DateTime(2026, 3, 22, 0, 0, 0, 0, DateTimeKind.Local), 1, 1, "Scheduled" },
                    { 2, new DateTime(2026, 3, 23, 0, 0, 0, 0, DateTimeKind.Local), 2, 2, "Scheduled" },
                    { 3, new DateTime(2026, 3, 24, 0, 0, 0, 0, DateTimeKind.Local), 3, 3, "Scheduled" },
                    { 4, new DateTime(2026, 3, 25, 0, 0, 0, 0, DateTimeKind.Local), 4, 4, "Scheduled" },
                    { 5, new DateTime(2026, 3, 26, 0, 0, 0, 0, DateTimeKind.Local), 5, 5, "Scheduled" },
                    { 6, new DateTime(2026, 3, 27, 0, 0, 0, 0, DateTimeKind.Local), 6, 6, "Scheduled" },
                    { 7, new DateTime(2026, 3, 28, 0, 0, 0, 0, DateTimeKind.Local), 7, 7, "Scheduled" },
                    { 8, new DateTime(2026, 3, 29, 0, 0, 0, 0, DateTimeKind.Local), 8, 8, "Scheduled" },
                    { 9, new DateTime(2026, 3, 30, 0, 0, 0, 0, DateTimeKind.Local), 9, 9, "Scheduled" },
                    { 10, new DateTime(2026, 3, 31, 0, 0, 0, 0, DateTimeKind.Local), 10, 10, "Scheduled" }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
