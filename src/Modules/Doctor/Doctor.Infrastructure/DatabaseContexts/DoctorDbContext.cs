using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.DatabaseContexts;

public sealed class DoctorDbContext(DbContextOptions<DoctorDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoctorDbContext).Assembly);
}
