using Hospital_Management_System.Model;
using Hospital_Management_System.UnitOfWork;
using Hospital_Management_System.Model.DTO.AppointmentDTOs; // هنا DTOs
using Hospital_Management_System.Model.DTO.PaymentDTOs; // هنا DTOs

using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Model.Service;

public class AppointmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ================= Book Appointment =================
    public async Task BookAsync(CreateAppointmentDto dto)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);

        if (patient == null) throw new Exception("Patient not found");
        if (doctor == null) throw new Exception("Doctor not found");

        var isBusy = await _unitOfWork.Appointments.Query
            .AnyAsync(a => a.DoctorId == dto.DoctorId && a.Date == dto.Date);

        if (isBusy)
            throw new Exception("Doctor already has appointment at this time");

        var appointment = new Appointment(patient, doctor, dto.Date);
        var payment = new Payment(0, dto.Amount); // Id مش مهم
        appointment.AssignPayment(payment);

        await _unitOfWork.Appointments.AddAsync(appointment);
        await _unitOfWork.Payments.AddAsync(payment);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Complete Appointment =================
    public async Task CompleteAsync(CompleteAppointmentDto dto)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.AppointmentId);

        if (appointment == null)
            throw new Exception("Appointment not found");

        appointment.UpdateStatus("Completed");
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Pay =================
    public async Task PayAsync(PayDto dto)
    {
        var appointment = await _unitOfWork.Appointments.Query
            .Include(a => a.Payment)
            .FirstOrDefaultAsync(a => a.Id == dto.AppointmentId);

        if (appointment == null)
            throw new Exception("Appointment not found");

        if (appointment.Payment == null)
            throw new Exception("No payment found");

        appointment.Payment.Pay(appointment.Id);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Get By Id =================
    public async Task<Appointment> GetByIdAsync(int id)
    {
        var appointment = await _unitOfWork.Appointments.Query
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.Payment)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment == null)
            throw new Exception("Appointment not found");

        return appointment;
    }

    // ================= Delete =================
    public async Task DeleteAsync(int id)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);

        if (appointment == null)
            throw new Exception("Appointment not found");

        await _unitOfWork.Appointments.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Pagination =================
    public async Task<List<Appointment>> GetPageAsync(AppointmentQueryDto dto)
    {
        IQueryable<Appointment> query = _unitOfWork.Appointments.Query
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.Payment);

        // 🔍 Filter
        if (!string.IsNullOrEmpty(dto.Status))
            query = query.Where(a => a.Status == dto.Status);

        if (dto.DoctorId.HasValue)
            query = query.Where(a => a.DoctorId == dto.DoctorId.Value);

        return await query
            .OrderBy(a => a.Date)
            .Skip((dto.PageNumber - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .ToListAsync();
    }
}