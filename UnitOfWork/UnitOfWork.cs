using HospitalManagementSystemAPIVersion.Model;
using HospitalManagementSystemAPIVersion.Repository;
namespace HospitalManagementSystemAPIVersion.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly HospitalDbContext _context;
    public UnitOfWork(HospitalDbContext context)
    {
        _context = context;

        Patients = new GenericRepository<Patient>(_context);
        Doctors = new GenericRepository<Doctor>(_context);
        Appointments = new GenericRepository<Appointment>(_context);
        Prescriptions = new GenericRepository<Prescription>(_context);
        Payments = new GenericRepository<Payment>(_context);
    }
    public IGenericRepository<Patient> Patients { get; }
    public IGenericRepository<Doctor> Doctors { get; }
    public IGenericRepository<Appointment> Appointments { get; }
    public IGenericRepository<Prescription> Prescriptions { get; }
    public IGenericRepository<Payment> Payments { get; }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}