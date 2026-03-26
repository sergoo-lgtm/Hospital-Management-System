using HospitalManagementSystemAPIVersion.DTO.DoctorDTOs;
using HospitalManagementSystemAPIVersion.Model;
using HospitalManagementSystemAPIVersion.UnitOfWork;
using HospitalManagementSystemAPIVersion.DTO;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPIVersion.Service;

public class DoctorService
{
    private readonly IUnitOfWork _unitOfWork;

    public DoctorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DoctorDto> AddAsync(CreateDoctorDto dto)
    {
        if (dto == null) 
            throw new ArgumentNullException(nameof(dto), "DTO must be provided");

        var doctor = new Doctor(dto.Name, dto.Specialization);
        await _unitOfWork.Doctors.AddAsync(doctor);
        await _unitOfWork.SaveChangesAsync();

        return new DoctorDto
        {
            Id = doctor.Id,
            Name = doctor.Name,
            Specialization = doctor.Specialization
        };
    }

    public async Task<DoctorDto> GetByIdAsync(int id)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
        if (doctor == null) throw new KeyNotFoundException("Doctor not found");

        return new DoctorDto
        {
            Id = doctor.Id,
            Name = doctor.Name,
            Specialization = doctor.Specialization
        };
    }

    public async Task DeleteAsync(int id)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
        if (doctor == null) throw new KeyNotFoundException("Doctor not found");

        await _unitOfWork.Doctors.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PagedResult<DoctorDto>> GetPageAsync(DoctorQueryDto dto)
    {
        var mappedQuery = _unitOfWork.Doctors.GetAll
            .WhereIf(!string.IsNullOrEmpty(dto.Search),
                d => d.Name.Contains(dto.Search))
            .WhereIf(!string.IsNullOrEmpty(dto.Specialization),
                d => d.Specialization.Contains(dto.Specialization))
            .OrderBy(d => d.Id)
            .Select(d => new DoctorDto
            {
                Id = d.Id,
                Name = d.Name,
                Specialization = d.Specialization
            });

        return await mappedQuery.ToPagedListAsync(dto.PageNumber, dto.PageSize);
    }
}