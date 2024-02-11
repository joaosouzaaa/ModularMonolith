using Appointment.ApplicationService.DataTransferObjects.Appointment;

namespace Appointment.ApplicationService.Interfaces.Services;
public interface IAppointmentTimeService
{
    Task<bool> AddAsync(AppointmentTimeSave appointmentTimeSave);
}
