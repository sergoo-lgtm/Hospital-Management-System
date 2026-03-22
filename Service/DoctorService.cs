using Hospital_Management_System.Model;
using Hospital_Management_System.UnitOfWork;
using Hospital_Management_System.Model.DTO;
using Hospital_Management_System.Model.DTO.DoctorDTOs;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Model.Service;

public class DoctorService
{
    private readonly IUnitOfWork _unitOfWork;

    public DoctorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ================= Add Doctor =================
    public async Task AddAsync(CreateDoctorDto dto)
    {
        var doctor = new Doctor(dto.Name, dto.Specialization);
        await _unitOfWork.Doctors.AddAsync(doctor);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Get By Id =================
    public async Task<Doctor> GetByIdAsync(int id)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
        if (doctor == null)
            throw new Exception("Doctor not found");
        return doctor;
    }

    // ================= Update Doctor =================
    public async Task UpdateAsync(UpdateDoctorDto dto)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.Id);
        if (doctor == null)
            throw new Exception("Doctor not found");

        doctor.SetProfile(dto.Name, dto.Specialization);
        await _unitOfWork.Doctors.UpdateAsync(doctor);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Delete Doctor =================
    public async Task DeleteAsync(int id)
    {
        var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
        if (doctor == null)
            throw new Exception("Doctor not found");

        await _unitOfWork.Doctors.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Get All =================
    public async Task<List<Doctor>> GetAllAsync()
    {
        return await _unitOfWork.Doctors.GetAllAsync();
    }

    // ================= Pagination =================
    public async Task<List<Doctor>> GetPageAsync(DoctorQueryDto dto)
    {
        return await _unitOfWork.Doctors.Query
            .OrderBy(d => d.Id)
            .Skip((dto.PageNumber - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .ToListAsync();
    }

    // ================= Pagination + Search =================
    public async Task<List<Doctor>> GetPageWithSearchAsync(DoctorQueryDto dto)
    {
        var query = _unitOfWork.Doctors.Query;

        if (!string.IsNullOrEmpty(dto.Search))
        {
            query = query.Where(d =>
                d.Name.Contains(dto.Search) ||
                d.Specialization.Contains(dto.Search));
        }

        return await query
            .OrderBy(d => d.Id)
            .Skip((dto.PageNumber - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .ToListAsync();
    }
}