using Appointment.Domain.Entities;

namespace Appointment.Infrastructure.Interfaces.Repositories;
public interface IAppointmentTimeRepository
{
    Task<bool> AddAsync(AppointmentTime appointmentTime);
    Task<bool> ExistsByTimeAndDoctorAsync(int doctorAttendantId, DateTime time);
}
