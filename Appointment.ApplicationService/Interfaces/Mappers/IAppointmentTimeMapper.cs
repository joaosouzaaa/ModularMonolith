using Appointment.ApplicationService.DataTransferObjects.Appointment;
using Appointment.Domain.Contracts;
using Appointment.Domain.Entities;

namespace Appointment.ApplicationService.Interfaces.Mappers;
public interface IAppointmentTimeMapper
{
    AppointmentTime SaveToDomain(AppointmentTimeSave appointmentTimeSave);
    AppointmentTimeCreatedEvent DomainToTimeCreatedEvent(AppointmentTime appointmentTime);
}
