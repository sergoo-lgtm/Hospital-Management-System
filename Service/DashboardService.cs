using HospitalManagementSystemAPIVersion.DTO;
using HospitalManagementSystemAPIVersion.Model;
using HospitalManagementSystemAPIVersion.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPIVersion.Service;

public class DashboardService
{
    private readonly HospitalDbContext _context;

    public DashboardService(HospitalDbContext context)
    {
        _context = context;
    }
    
    public async Task<DashboardDto> GetCountsAsync()
    {
        var result = _context.Set<DashboardDto>()
            .FromSqlRaw("EXEC GetDashboardCounts")
            .AsEnumerable()  
            .FirstOrDefault(); 
        return result;
    }
}