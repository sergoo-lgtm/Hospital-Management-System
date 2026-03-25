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

    public async Task<Prescription> AddAsync(CreatePrescriptionDto dto)
    {
        if (dto == null) 
            throw new ArgumentNullException(nameof(dto), "DTO must be provided");
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.AppointmentId);
        if (appointment == null) throw new KeyNotFoundException("Appointment not found");

        var prescription = new Prescription(appointment, dto.Notes, dto.Medications);
        await _unitOfWork.Prescriptions.AddAsync(prescription);
        await _unitOfWork.SaveChangesAsync();
        return prescription;
    }

    public async Task<Prescription> UpdateNotesAsync(UpdateNotesDto dto)
    {
        if (dto == null) 
            throw new ArgumentNullException(nameof(dto), "DTO must be provided");
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(dto.PrescriptionId);
        if (prescription == null) throw new KeyNotFoundException("Prescription not found");

        prescription.SetNotes(dto.Notes);
        await _unitOfWork.SaveChangesAsync();
        return prescription;
    }

    public async Task<Prescription> UpdateMedicationsAsync(UpdateMedicationsDto dto)
    {
        if (dto == null) 
            throw new ArgumentNullException(nameof(dto), "DTO must be provided");
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(dto.PrescriptionId);
        if (prescription == null) throw new KeyNotFoundException("Prescription not found");

        prescription.SetMedications(dto.Medications);
        await _unitOfWork.SaveChangesAsync();
        return prescription;
    }

    public async Task DeleteAsync(int id)
    {
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(id);
        if (prescription == null) throw new KeyNotFoundException("Prescription not found");

        await _unitOfWork.Prescriptions.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Prescription> GetByIdAsync(int id)
    {
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(id);
        if (prescription == null) throw new KeyNotFoundException("Prescription not found");
        return prescription;
    }

    public async Task<PagedResult<Prescription>> GetPageAsync(int pageNumber, int pageSize, string? search = null)
    {
        var query = _unitOfWork.Prescriptions.GetAll;

        query = query.WhereIf(!string.IsNullOrEmpty(search),
            p => p.Notes.Contains(search) || p.Medications.Contains(search));

        query = query.OrderBy(p => p.Id);

        return await query.ToPagedListAsync(pageNumber, pageSize);
    }
}