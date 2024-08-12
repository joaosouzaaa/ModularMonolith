using Microsoft.EntityFrameworkCore;

namespace Appointment.Infrastructure.DatabaseContexts;

public sealed class AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentDbContext).Assembly);
}
