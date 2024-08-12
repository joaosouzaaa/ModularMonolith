using Appointment.Domain.Entities;

namespace Appointment.Domain.Interfaces.Repositories;

public interface IAppointmentTimeRepository
{
    Task<bool> AddAsync(AppointmentTime appointmentTime, CancellationToken cancellationToken);
    Task<bool> ExistsByTimeAndDoctorAsync(int doctorAttendantId, DateTime time, CancellationToken cancellationToken);
}
