using Microsoft.EntityFrameworkCore;

namespace Patient.Infrastructure.DatabaseContexts;
public sealed class PatientDbContext(DbContextOptions<PatientDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PatientDbContext).Assembly);
    }
}
