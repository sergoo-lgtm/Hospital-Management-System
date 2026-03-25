using HospitalManagementSystemAPIVersion.Model;
using HospitalManagementSystemAPIVersion.DTO.PrescriptionDTOs;
using HospitalManagementSystemAPIVersion.DTO;
using HospitalManagementSystemAPIVersion.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPIVersion.Service;

public class PrescriptionService
{
    private readonly IUnitOfWork _unitOfWork;

    public PrescriptionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PrescriptionDto> AddAsync(CreatePrescriptionDto dto)
    {
        if (dto == null) 
            throw new ArgumentNullException(nameof(dto), "DTO must be provided");

        var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.AppointmentId);
        if (appointment == null) 
            throw new KeyNotFoundException("Appointment not found");

        var prescription = new Prescription(appointment, dto.Notes, dto.Medications);
        await _unitOfWork.Prescriptions.AddAsync(prescription);
        await _unitOfWork.SaveChangesAsync();

        return new PrescriptionDto
        {
            Id = prescription.Id,
            AppointmentId = prescription.AppointmentId,
            Notes = prescription.Notes,
            Medications = prescription.Medications
        };
    }

    public async Task<PrescriptionDto> UpdateNotesAsync(UpdateNotesDto dto)
    {
        if (dto == null) 
            throw new ArgumentNullException(nameof(dto), "DTO must be provided");

        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(dto.PrescriptionId);
        if (prescription == null) 
            throw new KeyNotFoundException("Prescription not found");

        prescription.SetNotes(dto.Notes);
        await _unitOfWork.SaveChangesAsync();

        return new PrescriptionDto
        {
            Id = prescription.Id,
            AppointmentId = prescription.AppointmentId,
            Notes = prescription.Notes,
            Medications = prescription.Medications
        };
    }

    public async Task<PrescriptionDto> UpdateMedicationsAsync(UpdateMedicationsDto dto)
    {
        if (dto == null) 
            throw new ArgumentNullException(nameof(dto), "DTO must be provided");

        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(dto.PrescriptionId);
        if (prescription == null) 
            throw new KeyNotFoundException("Prescription not found");

        prescription.SetMedications(dto.Medications);
        await _unitOfWork.SaveChangesAsync();

        return new PrescriptionDto
        {
            Id = prescription.Id,
            AppointmentId = prescription.AppointmentId,
            Notes = prescription.Notes,
            Medications = prescription.Medications
        };
    }

    public async Task DeleteAsync(int id)
    {
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(id);
        if (prescription == null) 
            throw new KeyNotFoundException("Prescription not found");

        await _unitOfWork.Prescriptions.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PrescriptionDto> GetByIdAsync(int id)
    {
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(id);
        if (prescription == null) 
            throw new KeyNotFoundException("Prescription not found");

        return new PrescriptionDto
        {
            Id = prescription.Id,
            AppointmentId = prescription.AppointmentId,
            Notes = prescription.Notes,
            Medications = prescription.Medications
        };
    }

    public async Task<PagedResult<PrescriptionDto>> GetPageAsync(int pageNumber, int pageSize, string? search = null)
    {
        var mappedQuery = _unitOfWork.Prescriptions.GetAll
            .WhereIf(!string.IsNullOrEmpty(search),
                p => p.Notes.Contains(search) || p.Medications.Contains(search))
            .OrderBy(p => p.Id)
            .Select(p => new PrescriptionDto
            {
                Id = p.Id,
                AppointmentId = p.AppointmentId,
                Notes = p.Notes,
                Medications = p.Medications
            });

        var totalCount = await mappedQuery.CountAsync();
        var items = await mappedQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<PrescriptionDto>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}