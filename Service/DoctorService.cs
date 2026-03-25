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

    public async Task<Doctor> AddAsync(CreateDoctorDto dto)
    {
        if (dto == null) 
            throw new ArgumentNullException(nameof(dto), "DTO must be provided");
        var doctor = new Doctor(dto.Name, dto.Specialization);
        await _unitOfWork.Doctors.AddAsync(doctor);
        await _unitOfWork.SaveChangesAsync();
        return doctor;
    }

    public async Task<Doctor> GetByIdAsync(int id)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
        if (doctor == null) throw new KeyNotFoundException("Doctor not found");
        return doctor;
    }

    public async Task DeleteAsync(int id)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
        if (doctor == null) throw new KeyNotFoundException("Doctor not found");
        await _unitOfWork.Doctors.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PagedResult<Doctor>> GetPageAsync(DoctorQueryDto dto)
    {
        var query = _unitOfWork.Doctors.GetAll;

        query = query.WhereIf(!string.IsNullOrEmpty(dto.Search),
            d => d.Name.Contains(dto.Search));

        query = query.WhereIf(!string.IsNullOrEmpty(dto.Specialization),
            d => d.Specialization.Contains(dto.Specialization));

        var mappedQuery = query.OrderBy(d => d.Id);

        return await mappedQuery.ToPagedListAsync(dto.PageNumber, dto.PageSize);
    }
}