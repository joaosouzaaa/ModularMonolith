using Appointment.Domain.Entities;
using Appointment.Domain.Interfaces.Repositories;
using Appointment.Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Infrastructure.Repositories;

public sealed class AppointmentTimeRepository(AppointmentDbContext dbContext) : IAppointmentTimeRepository, IDisposable
{
    private DbSet<AppointmentTime> DbContextSet => dbContext.Set<AppointmentTime>();

    public async Task<bool> AddAsync(AppointmentTime appointmentTime, CancellationToken cancellationToken)
    {
        await DbContextSet.AddAsync(appointmentTime, cancellationToken);

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public Task<bool> ExistsByTimeAndDoctorAsync(int doctorAttendantId, DateTime time, CancellationToken cancellationToken) =>
        DbContextSet.AnyAsync(a => a.DoctorAttendantId == doctorAttendantId && a.Time == time, cancellationToken);

    public void Dispose()
    {
        dbContext.Dispose();

        GC.SuppressFinalize(this);
    }
}
