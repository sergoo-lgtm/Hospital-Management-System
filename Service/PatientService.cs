using Hospital_Management_System.Model;
using Hospital_Management_System.UnitOfWork;

using Hospital_Management_System.Model.DTO.patientDTOs;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Model.Service;

public class PatientService
{
    private readonly IUnitOfWork _unitOfWork;

    public PatientService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ================= Add Patient =================
    public async Task AddAsync(CreatePatientDto dto)
    {
        var patient = new Patient(dto.Name, dto.Phone);
        await _unitOfWork.Patients.AddAsync(patient);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Get By Id =================
    public async Task<Patient> GetByIdAsync(int id)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(id);
        if (patient == null)
            throw new Exception("Patient not found");
        return patient;
    }

    // ================= Update Patient =================
    public async Task UpdateAsync(UpdatePatientDto dto)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(dto.Id);
        if (patient == null)
            throw new Exception("Patient not found");

        patient.SetName(dto.Name);
        patient.SetPhone(dto.Phone);

        await _unitOfWork.Patients.UpdateAsync(patient);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Delete Patient =================
    public async Task DeleteAsync(int id)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(id);
        if (patient == null)
            throw new Exception("Patient not found");

        await _unitOfWork.Patients.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Get All =================
    public async Task<List<Patient>> GetAllAsync()
    {
        return await _unitOfWork.Patients.GetAllAsync();
    }

    // ================= Pagination Only =================
    public async Task<List<Patient>> GetPageAsync(PatientQueryDto dto)
    {
        return await _unitOfWork.Patients.Query
            .OrderBy(p => p.Id)
            .Skip((dto.PageNumber - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .ToListAsync();
    }

    // ================= Pagination + Search =================
    public async Task<List<Patient>> GetPageWithSearchAsync(PatientQueryDto dto)
    {
        var query = _unitOfWork.Patients.Query;

        if (!string.IsNullOrEmpty(dto.Search))
        {
            query = query.Where(p => p.Name.Contains(dto.Search));
        }

        return await query
            .OrderBy(p => p.Id)
            .Skip((dto.PageNumber - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .ToListAsync();
    }
}