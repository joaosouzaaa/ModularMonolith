using Appointment.Domain.Entities;
using Appointment.Domain.Interfaces.Repositories;
using Appointment.Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Infrastructure.Repositories;

public sealed class AppointmentTimeRepository(AppointmentDbContext dbContext) : IAppointmentTimeRepository
{
    private DbSet<AppointmentTime> DbContextSet => dbContext.Set<AppointmentTime>();

    public async Task<bool> AddAsync(AppointmentTime appointmentTime)
    {
        await DbContextSet.AddAsync(appointmentTime);

        return await dbContext.SaveChangesAsync() > 0;
    }

    public Task<bool> ExistsByTimeAndDoctorAsync(int doctorAttendantId, DateTime time) =>
        DbContextSet.AnyAsync(a => a.DoctorAttendantId == doctorAttendantId && a.Time == time);
}
