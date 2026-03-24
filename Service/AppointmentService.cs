using HospitalManagementSystemAPIVersion.Model;
using HospitalManagementSystemAPIVersion.DTO.AppointmentDTOs;
using HospitalManagementSystemAPIVersion.DTO;
using HospitalManagementSystemAPIVersion.DTO.PaymentDTOs;


using HospitalManagementSystemAPIVersion.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPIVersion.Service;

public class AppointmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Appointment> AddAsync(CreateAppointmentDto dto)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);
        if (patient == null) throw new KeyNotFoundException("Patient not found");

        var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);
        if (doctor == null) throw new KeyNotFoundException("Doctor not found");

        var appointment = new Appointment(patient, doctor, dto.Date);

        var payment = new Payment(dto.Amount, appointment.Id);
        appointment.AssignPayment(payment);

        await _unitOfWork.Appointments.AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        return appointment;
    }

    public async Task<Appointment> CompleteAsync(CompleteAppointmentDto dto)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.AppointmentId);
        if (appointment == null) throw new KeyNotFoundException("Appointment not found");

        if (appointment.Payment != null)
            appointment.Payment.Pay(appointment.Id);


        await _unitOfWork.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment> GetByIdAsync(int id)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
        if (appointment == null) throw new KeyNotFoundException("Appointment not found");
        return appointment;
    }

    public async Task DeleteAsync(int id)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
        if (appointment == null) throw new KeyNotFoundException("Appointment not found");

        await _unitOfWork.Appointments.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PagedResult<Appointment>> GetPageAsync(AppointmentQueryDto dto)
    {
        var query = _unitOfWork.Appointments.GetAll;

        query = query.WhereIf(!string.IsNullOrEmpty(dto.Status),
            a => a.Status.Contains(dto.Status));

        query = query.WhereIf(dto.DoctorId.HasValue,
            a => a.DoctorId == dto.DoctorId.Value);

        query = query.OrderBy(a => a.Date);

        return await query.ToPagedListAsync(dto.PageNumber, dto.PageSize);
    }
}