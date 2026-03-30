using HospitalManagementSystemAPIVersion.DTO;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPIVersion.Model;

public class HospitalDbContext : DbContext
{
    public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<AppointmentDetailsDto> AppointmentDetailsDto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ================= Patients =================
        modelBuilder.Entity<Patient>().HasData(
            new { Id = 1, Name = "Ahmed Ali", Phone = "01012345678", Email = "yousefserag32@yahoo.com" },
            new { Id = 2, Name = "Sara Hassan", Phone = "01023456789", Email = "yousefserag32@yahoo.com" },
            new { Id = 3, Name = "Mohamed Tamer", Phone = "01034567890", Email = "yousefserag32@yahoo.com" },
            new { Id = 4, Name = "Laila Fathy", Phone = "01045678901", Email = "yousefserag32@yahoo.com" },
            new { Id = 5, Name = "Omar Mostafa", Phone = "01056789012", Email = "yousefserag32@yahoo.com" },
            new { Id = 6, Name = "Nour El-Din", Phone = "01067890123", Email = "yousefserag32@yahoo.com" },
            new { Id = 7, Name = "Hana Mahmoud", Phone = "01078901234", Email = "yousefserag32@yahoo.com" },
            new { Id = 8, Name = "Youssef Samir", Phone = "01089012345", Email = "yousefserag32@yahoo.com" },
            new { Id = 9, Name = "Mona Kamal", Phone = "01090123456", Email = "yousefserag32@yahoo.com" },
            new { Id = 10, Name = "Khaled Adel", Phone = "01001234567", Email = "yousefserag32@yahoo.com" }
        );

        // ================= Doctors =================
        modelBuilder.Entity<Doctor>().HasData(
            new { Id = 1, Name = "Dr. Ahmed Hossam", Specialization = "Cardiology" },
            new { Id = 2, Name = "Dr. Sara Khaled", Specialization = "Dermatology" },
            new { Id = 3, Name = "Dr. Mohamed Adel", Specialization = "Neurology" },
            new { Id = 4, Name = "Dr. Laila Samir", Specialization = "Pediatrics" },
            new { Id = 5, Name = "Dr. Omar Fathy", Specialization = "Orthopedics" },
            new { Id = 6, Name = "Dr. Nour Hassan", Specialization = "General Surgery" },
            new { Id = 7, Name = "Dr. Hana Tamer", Specialization = "Gynecology" },
            new { Id = 8, Name = "Dr. Youssef Mahmoud", Specialization = "Ophthalmology" },
            new { Id = 9, Name = "Dr. Mona Kamal", Specialization = "ENT" },
            new { Id = 10, Name = "Dr. Khaled Ali", Specialization = "Psychiatry" }
        );

        // ================= Appointments =================
        modelBuilder.Entity<Appointment>().HasData(
            new { Id = 1, PatientId = 1, DoctorId = 1, Date = DateTime.Today, Status = "Scheduled" },
            new { Id = 2, PatientId = 2, DoctorId = 2, Date = DateTime.Today.AddDays(1), Status = "Scheduled" },
            new { Id = 3, PatientId = 3, DoctorId = 3, Date = DateTime.Today.AddDays(2), Status = "Scheduled" },
            new { Id = 4, PatientId = 4, DoctorId = 4, Date = DateTime.Today.AddDays(3), Status = "Scheduled" },
            new { Id = 5, PatientId = 5, DoctorId = 5, Date = DateTime.Today.AddDays(4), Status = "Scheduled" },
            new { Id = 6, PatientId = 6, DoctorId = 6, Date = DateTime.Today.AddDays(5), Status = "Scheduled" },
            new { Id = 7, PatientId = 7, DoctorId = 7, Date = DateTime.Today.AddDays(6), Status = "Scheduled" },
            new { Id = 8, PatientId = 8, DoctorId = 8, Date = DateTime.Today.AddDays(7), Status = "Scheduled" },
            new { Id = 9, PatientId = 9, DoctorId = 9, Date = DateTime.Today.AddDays(8), Status = "Scheduled" },
            new { Id = 10, PatientId = 10, DoctorId = 10, Date = DateTime.Today.AddDays(9), Status = "Scheduled" }
        );

        // ================= Payments =================
        modelBuilder.Entity<Payment>().HasData(
            new { Id = 1, AppointmentId = 1, Amount = 110, IsPaid = false },
            new { Id = 2, AppointmentId = 2, Amount = 120, IsPaid = false },
            new { Id = 3, AppointmentId = 3, Amount = 130, IsPaid = false },
            new { Id = 4, AppointmentId = 4, Amount = 140, IsPaid = false },
            new { Id = 5, AppointmentId = 5, Amount = 150, IsPaid = false },
            new { Id = 6, AppointmentId = 6, Amount = 160, IsPaid = false },
            new { Id = 7, AppointmentId = 7, Amount = 170, IsPaid = false },
            new { Id = 8, AppointmentId = 8, Amount = 180, IsPaid = false },
            new { Id = 9, AppointmentId = 9, Amount = 190, IsPaid = false },
            new { Id = 10, AppointmentId = 10, Amount = 200, IsPaid = false }
        );

        // ================= Prescriptions =================
        modelBuilder.Entity<Prescription>().HasData(
            new { Id = 1, AppointmentId = 1, Notes = "Patient has mild fever", Medications = "Paracetamol 500mg" },
            new { Id = 2, AppointmentId = 2, Notes = "Patient has cough", Medications = "Cough Syrup" },
            new { Id = 3, AppointmentId = 3, Notes = "Patient has headache", Medications = "Ibuprofen" },
            new { Id = 4, AppointmentId = 4, Notes = "Patient has flu", Medications = "Vitamin C" },
            new { Id = 5, AppointmentId = 5, Notes = "Patient has cold", Medications = "Antihistamine" },
            new { Id = 6, AppointmentId = 6, Notes = "Patient has back pain", Medications = "Muscle Relaxant" },
            new { Id = 7, AppointmentId = 7, Notes = "Patient has stomach ache", Medications = "Antacid" },
            new { Id = 8, AppointmentId = 8, Notes = "Patient has allergies", Medications = "Antihistamine" },
            new { Id = 9, AppointmentId = 9, Notes = "Patient has migraine", Medications = "Painkiller" },
            new { Id = 10, AppointmentId = 10, Notes = "Patient has infection", Medications = "Antibiotic" }
        );

        modelBuilder.Entity<DashboardDto>().HasNoKey();
        modelBuilder.Entity<AppointmentDetailsDto>(A =>
        {
            A.HasNoKey();
            A.ToView("vw_AppointmentDetails");
        });
    }
}