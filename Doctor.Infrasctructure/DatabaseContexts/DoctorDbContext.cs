using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrasctructure.DatabaseContexts;
public sealed class DoctorDbContext(DbContextOptions<DoctorDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoctorDbContext).Assembly);
    }
}
