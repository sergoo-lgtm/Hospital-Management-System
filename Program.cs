using Hospital_Management_System.Model.Service;
using Hospital_Management_System.UnitOfWork;
using Hospital_Management_System.Model.DTO.patientDTOs;
using Hospital_Management_System.Model.DTO.DoctorDTOs;
using Hospital_Management_System.Model.DTO.AppointmentDTOs;
using Hospital_Management_System.Model.DTO.PaymentDTOs;
using Hospital_Management_System.Model.DTO.PrescriptionDTOs;
using Hospital_Management_System.Model;
using Serilog;

namespace Hospital_Management_System;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // تهيئة Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("hospital_log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            using var dbContext = new HospitalDbContext();
            var unitOfWork = new UnitOfWork.UnitOfWork(dbContext);

            var patientService = new PatientService(unitOfWork);
            var doctorService = new DoctorService(unitOfWork);
            var appointmentService = new AppointmentService(unitOfWork);
            var paymentService = new PaymentService(unitOfWork);
            var prescriptionService = new PrescriptionService(unitOfWork);

            Console.WriteLine("==== Welcome to Hospital Management Console App ====");
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nSelect operation:");
                Console.WriteLine("1. Patients CRUD + Pagination");
                Console.WriteLine("2. Doctors CRUD + Pagination");
                Console.WriteLine("3. Appointments CRUD + Pagination");
                Console.WriteLine("4. Payments CRUD + Pagination");
                Console.WriteLine("5. Prescriptions CRUD + Pagination");
                Console.WriteLine("0. Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await PatientMenu(patientService);
                        break;
                    case "2":
                        await DoctorMenu(doctorService);
                        break;
                    case "3":
                        await AppointmentMenu(appointmentService, patientService, doctorService, paymentService);
                        break;
                    case "4":
                        await PaymentMenu(paymentService);
                        break;
                    case "5":
                        await PrescriptionMenu(prescriptionService, appointmentService);
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
            Log.Fatal(ex, "Fatal error in Main method");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    // ================= Patient Menu =================
    private static async Task PatientMenu(PatientService service)
    {
        bool back = false;
        int pageNumber = 1;
        const int pageSize = 5;

        while (!back)
        {
            Console.WriteLine("\n--- Patients Menu ---");
            Console.WriteLine("1. List Patients (Pagination)");
            Console.WriteLine("2. Add Patient");
            Console.WriteLine("3. Update Patient");
            Console.WriteLine("4. Delete Patient");
            Console.WriteLine("0. Back");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    bool listing = true;
                    while (listing)
                    {
                        try
                        {
                            var patients = await service.GetPageAsync(new PatientQueryDto
                            {
                                PageNumber = pageNumber,
                                PageSize = pageSize
                            });

                            Console.WriteLine($"\n--- Page {pageNumber} ---");
                            foreach (var p in patients)
                            {
                                Console.WriteLine($"Id: {p.Id}, Name: {p.Name}, Phone: {p.Phone}");
                            }

                            Console.WriteLine("\nCommands: n=next, p=prev, b=back");
                            var cmd = Console.ReadLine();
                            if (cmd == "n") pageNumber++;
                            else if (cmd == "p" && pageNumber > 1) pageNumber--;
                            else if (cmd == "b") listing = false;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error listing patients: {ex.Message}");
                            Log.Error(ex, "Error listing patients");
                        }
                    }
                    break;

                case "2":
                    Console.Write("Enter name: ");
                    var name = Console.ReadLine();
                    Console.Write("Enter phone (11 digits): ");
                    var phone = Console.ReadLine();

                    try
                    {
                        await service.AddAsync(new CreatePatientDto { Name = name, Phone = phone });
                        Console.WriteLine("Patient added successfully!");
                        Log.Information("Patient added: {Name}", name);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error adding patient: {ex.Message}");
                        Log.Error(ex, "Error adding patient: {Name}", name);
                    }
                    break;

                case "3":
                    try
                    {
                        Console.Write("Enter patient Id to update: ");
                        var idUpdate = int.Parse(Console.ReadLine());
                        Console.Write("Enter new name: ");
                        var newName = Console.ReadLine();
                        Console.Write("Enter new phone: ");
                        var newPhone = Console.ReadLine();

                        await service.UpdateAsync(new UpdatePatientDto { Id = idUpdate, Name = newName, Phone = newPhone });
                        Console.WriteLine("Patient updated successfully!");
                        Log.Information("Patient updated: Id={Id}, Name={Name}", idUpdate, newName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating patient: {ex.Message}");
                        Log.Error(ex, "Error updating patient");
                    }
                    break;

                case "4":
                    try
                    {
                        Console.Write("Enter patient Id to delete: ");
                        var idDelete = int.Parse(Console.ReadLine());
                        await service.DeleteAsync(idDelete);
                        Console.WriteLine("Patient deleted successfully!");
                        Log.Information("Patient deleted: Id={Id}", idDelete);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting patient: {ex.Message}");
                        Log.Error(ex, "Error deleting patient");
                    }
                    break;

                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    // ================= Doctor Menu =================
    private static async Task DoctorMenu(DoctorService service)
    {
        bool back = false;
        int pageNumber = 1;
        const int pageSize = 5;

        while (!back)
        {
            Console.WriteLine("\n--- Doctors Menu ---");
            Console.WriteLine("1. List Doctors (Pagination)");
            Console.WriteLine("2. Add Doctor");
            Console.WriteLine("3. Update Doctor");
            Console.WriteLine("4. Delete Doctor");
            Console.WriteLine("0. Back");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    bool listing = true;
                    while (listing)
                    {
                        try
                        {
                            var doctors = await service.GetPageAsync(new DoctorQueryDto
                            {
                                PageNumber = pageNumber,
                                PageSize = pageSize
                            });

                            Console.WriteLine($"\n--- Page {pageNumber} ---");
                            foreach (var d in doctors)
                            {
                                Console.WriteLine($"Id: {d.Id}, Name: {d.Name}, Specialization: {d.Specialization}");
                            }

                            Console.WriteLine("\nCommands: n=next, p=prev, b=back");
                            var cmd = Console.ReadLine();
                            if (cmd == "n") pageNumber++;
                            else if (cmd == "p" && pageNumber > 1) pageNumber--;
                            else if (cmd == "b") listing = false;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error listing doctors: {ex.Message}");
                            Log.Error(ex, "Error listing doctors");
                        }
                    }
                    break;

                case "2":
                    Console.Write("Enter name: ");
                    var name = Console.ReadLine();
                    Console.Write("Enter specialization: ");
                    var spec = Console.ReadLine();

                    try
                    {
                        await service.AddAsync(new CreateDoctorDto { Name = name, Specialization = spec });
                        Console.WriteLine("Doctor added successfully!");
                        Log.Information("Doctor added: {Name}", name);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error adding doctor: {ex.Message}");
                        Log.Error(ex, "Error adding doctor: {Name}", name);
                    }
                    break;

                case "3":
                    try
                    {
                        Console.Write("Enter doctor Id to update: ");
                        var idUpdate = int.Parse(Console.ReadLine());
                        Console.Write("Enter new name: ");
                        var newName = Console.ReadLine();
                        Console.Write("Enter new specialization: ");
                        var newSpec = Console.ReadLine();

                        await service.UpdateAsync(new UpdateDoctorDto { Id = idUpdate, Name = newName, Specialization = newSpec });
                        Console.WriteLine("Doctor updated successfully!");
                        Log.Information("Doctor updated: Id={Id}, Name={Name}", idUpdate, newName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating doctor: {ex.Message}");
                        Log.Error(ex, "Error updating doctor");
                    }
                    break;

                case "4":
                    try
                    {
                        Console.Write("Enter doctor Id to delete: ");
                        var idDelete = int.Parse(Console.ReadLine());
                        await service.DeleteAsync(idDelete);
                        Console.WriteLine("Doctor deleted successfully!");
                        Log.Information("Doctor deleted: Id={Id}", idDelete);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting doctor: {ex.Message}");
                        Log.Error(ex, "Error deleting doctor");
                    }
                    break;

                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
    
    // ================= Appointment Menu =================
    private static async Task AppointmentMenu(AppointmentService appointmentService, PatientService patientService, DoctorService doctorService, PaymentService paymentService)
    {
        Console.WriteLine("Appointment menu here...");
    }

    // ================= Payment Menu =================
    private static async Task PaymentMenu(PaymentService service)
    {
        Console.WriteLine("Payment menu here...");
    }

    // ================= Prescription Menu =================
    private static async Task PrescriptionMenu(PrescriptionService service, AppointmentService appointmentService)
    {
        Console.WriteLine("Prescription menu here...");
    }
}