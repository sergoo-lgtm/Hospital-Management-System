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

    public async Task<AppointmentDto> AddAsync(CreateAppointmentDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "DTO must be provided");
        
        var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);
        if (patient == null) throw new KeyNotFoundException("Patient not found");

        var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);
        if (doctor == null) throw new KeyNotFoundException("Doctor not found");

        var appointment = new Appointment(patient, doctor, dto.Date);
        var payment = new Payment(dto.Amount, appointment.Id);
        appointment.AssignPayment(payment);

        await _unitOfWork.Appointments.AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        return new AppointmentDto
        {
            Id = appointment.Id,
            Date = appointment.Date,
            Status = appointment.Status,
            PatientId = appointment.PatientId,
            DoctorId = appointment.DoctorId,
            PaymentId = payment.Id,
            PrescriptionId = null
        };
    }

    public async Task<AppointmentDto> CompleteAsync(CompleteAppointmentDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "DTO must be provided");

        var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.AppointmentId);
        if (appointment == null) throw new KeyNotFoundException("Appointment not found");

        if (appointment.Payment != null)
            appointment.Payment.Pay(appointment.Id);

        await _unitOfWork.SaveChangesAsync();

        return new AppointmentDto
        {
            Id = appointment.Id,
            Date = appointment.Date,
            Status = appointment.Status,
            PatientId = appointment.PatientId,
            DoctorId = appointment.DoctorId,
            PaymentId = appointment.Payment?.Id,
            PrescriptionId = appointment.Prescription?.Id
        };
    }

    public async Task<AppointmentDto> GetByIdAsync(int id)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
        if (appointment == null) throw new KeyNotFoundException("Appointment not found");

        return new AppointmentDto
        {
            Id = appointment.Id,
            Date = appointment.Date,
            Status = appointment.Status,
            PatientId = appointment.PatientId,
            DoctorId = appointment.DoctorId,
            PaymentId = appointment.Payment?.Id,
            PrescriptionId = appointment.Prescription?.Id
        };
    }

    public async Task DeleteAsync(int id)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
        if (appointment == null) throw new KeyNotFoundException("Appointment not found");

        await _unitOfWork.Appointments.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PagedResult<AppointmentDto>> GetPageAsync(AppointmentQueryDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "DTO must be provided");

        var mappedQuery = _unitOfWork.Appointments.GetAll
            .WhereIf(!string.IsNullOrEmpty(dto.Status),
                a => a.Status.Contains(dto.Status))
            .WhereIf(dto.DoctorId.HasValue,
                a => a.DoctorId == dto.DoctorId.Value)
            .OrderBy(a => a.Date)
            .Select(a => new AppointmentDto
            {
                Id = a.Id,
                Date = a.Date,
                Status = a.Status,
                PatientId = a.PatientId,
                DoctorId = a.DoctorId,
                PaymentId = a.Payment != null ? a.Payment.Id : (int?)null,
                PrescriptionId = a.Prescription != null ? a.Prescription.Id : (int?)null
            });

        return await mappedQuery.ToPagedListAsync(dto.PageNumber, dto.PageSize);
    }
}