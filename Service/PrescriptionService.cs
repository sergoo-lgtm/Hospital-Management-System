using Hospital_Management_System.Model;
using Hospital_Management_System.UnitOfWork;
using Hospital_Management_System.Model.DTO.PrescriptionDTOs;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Model.Service;

public class PrescriptionService
{
    private readonly IUnitOfWork _unitOfWork;

    public PrescriptionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ================= Create Prescription =================
    public async Task CreateAsync(CreatePrescriptionDto dto)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.AppointmentId);
        if (appointment == null)
            throw new Exception("Appointment not found");

        var prescription = new Prescription(appointment, dto.Notes, dto.Medications);
        appointment.AssignPrescription(prescription);

        await _unitOfWork.Prescriptions.AddAsync(prescription);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Update Notes =================
    public async Task UpdateNotesAsync(UpdateNotesDto dto)
    {
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(dto.PrescriptionId);
        if (prescription == null)
            throw new Exception("Prescription not found");

        prescription.SetNotes(dto.Notes);

        await _unitOfWork.Prescriptions.UpdateAsync(prescription);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Update Medications =================
    public async Task UpdateMedicationsAsync(UpdateMedicationsDto dto)
    {
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(dto.PrescriptionId);
        if (prescription == null)
            throw new Exception("Prescription not found");

        prescription.SetMedications(dto.Medications);

        await _unitOfWork.Prescriptions.UpdateAsync(prescription);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Get By Id =================
    public async Task<Prescription> GetByIdAsync(int id)
    {
        var prescription = await _unitOfWork.Prescriptions.Query
            .Include(p => p.Appointment)
                .ThenInclude(a => a.Patient)
            .Include(p => p.Appointment)
                .ThenInclude(a => a.Doctor)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (prescription == null)
            throw new Exception("Prescription not found");

        return prescription;
    }

    // ================= Delete =================
    public async Task DeleteAsync(int id)
    {
        var prescription = await _unitOfWork.Prescriptions.GetByIdAsync(id);
        if (prescription == null)
            throw new Exception("Prescription not found");

        await _unitOfWork.Prescriptions.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Pagination =================
    public async Task<List<Prescription>> GetPageAsync(int pageNumber, int pageSize = 5)
    {
        return await _unitOfWork.Prescriptions.Query
            .Include(p => p.Appointment)
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}