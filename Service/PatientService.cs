using HospitalManagementSystemAPIVersion.UnitOfWork;
using HospitalManagementSystemAPIVersion.DTO.PatientDTOs;
using HospitalManagementSystemAPIVersion.Model;
using HospitalManagementSystemAPIVersion.DTO;

using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPIVersion.Service;

public class PatientService
{
    private readonly IUnitOfWork _unitOfWork;

    public PatientService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PatientDto> AddAsync(CreatePatientDto dto)
    {
        var patient = new Patient(dto.Name, dto.Phone);

        await _unitOfWork.Patients.AddAsync(patient);
        await _unitOfWork.SaveChangesAsync();

        return new PatientDto
        {
            Id = patient.Id,
            Name = patient.Name,
            Phone = patient.Phone
        };
    }

    public async Task<PatientDto> GetByIdAsync(int id)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(id);

        if (patient == null)
            throw new KeyNotFoundException("Patient not found");

        return new PatientDto
        {
            Id = patient.Id,
            Name = patient.Name,
            Phone = patient.Phone
        };
    }

    public async Task DeleteAsync(int id)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(id);

        if (patient == null)
            throw new KeyNotFoundException("Patient not found");

        await _unitOfWork.Patients.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PagedResult<PatientDto>> GetPageAsync(PatientQueryDto dto)
    {
        var query = _unitOfWork.Patients.GetAll;

        query = query.WhereIf(!string.IsNullOrEmpty(dto.Search),
            p => p.Name.Contains(dto.Search));

        var mappedQuery = query.OrderBy(p => p.Id)
            .Select(p => new PatientDto
            {
                Id = p.Id,
                Name = p.Name,
                Phone = p.Phone
            });

        return await mappedQuery.ToPagedListAsync(dto.PageNumber, dto.PageSize);
    }
}