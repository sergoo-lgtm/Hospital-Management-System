using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HospitalManagementSystemAPIVersion.Model;

public class HospitalDbContextFactory : IDesignTimeDbContextFactory<HospitalDbContext>
{
    public HospitalDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=HospitalSystemNew;User Id=sa;Password=Yosuef@2026;TrustServerCertificate=True;"
        );

        return new HospitalDbContext(optionsBuilder.Options);
    }
}