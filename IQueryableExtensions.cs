using HospitalManagementSystemAPIVersion.DTO;
namespace HospitalManagementSystemAPIVersion;
using Microsoft.EntityFrameworkCore;
public static class IQueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedListAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize
    )
    {
        var result = new PagedResult<T>();
        result.TotalCount = await query.CountAsync(); 
        result.Items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(); 
        return result;
    }
}