using HospitalManagementSystemAPIVersion.DTO;
using HospitalManagementSystemAPIVersion.Model;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPIVersion.Service;

public class AppointmentViewService
{
    private readonly HospitalDbContext _context;

    public AppointmentViewService(HospitalDbContext context)
    {
        _context = context;
    }

    public List<AppointmentDetailsDto> GetAppointmentDetails()
    {
        var result = _context.Set<AppointmentDetailsDto>()
            .FromSqlRaw("SELECT * FROM vw_AppointmentDetails")
            .AsNoTracking()
            .ToList();

        return result;
    }
}
