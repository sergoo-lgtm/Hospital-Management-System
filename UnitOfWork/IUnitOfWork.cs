using Hospital_Management_System.Model;
using Hospital_Management_System.Repository;
namespace Hospital_Management_System.UnitOfWork;

public interface IUnitOfWork
{
    IGenericRepository<Patient> Patients { get; }
    IGenericRepository<Doctor> Doctors { get; }
    IGenericRepository<Appointment> Appointments { get; }
    IGenericRepository<Prescription> Prescriptions { get; }
    IGenericRepository<Payment>  Payments { get; }
    Task SaveChangesAsync();

}