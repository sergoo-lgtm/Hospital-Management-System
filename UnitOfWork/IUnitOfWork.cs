using HospitalManagementSystemAPIVersion.Model;
using HospitalManagementSystemAPIVersion.Repository;
namespace HospitalManagementSystemAPIVersion.UnitOfWork;

public interface IUnitOfWork
{
    IGenericRepository<Patient> Patients { get; }
    IGenericRepository<Doctor> Doctors { get; }
    IGenericRepository<Appointment> Appointments { get; }
    IGenericRepository<Prescription> Prescriptions { get; }
    IGenericRepository<Payment>  Payments { get; }
    Task SaveChangesAsync();
}