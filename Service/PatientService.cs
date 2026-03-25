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
        var mappedQuery = _unitOfWork.Patients.GetAll
            .WhereIf(!string.IsNullOrEmpty(dto.Search), p => p.Name.Contains(dto.Search))
            .OrderBy(p => p.Id)
            .Select(p => new PatientDto
            {
                Id = p.Id,
                Name = p.Name,
                Phone = p.Phone
            });

        var totalCount = await mappedQuery.CountAsync();
        var items = await mappedQuery
            .Skip((dto.PageNumber - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .ToListAsync();

        return new PagedResult<PatientDto>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
    
}