using Appointment.Domain.Contracts;
using Appointment.Domain.DataTransferObjects.Appointment;
using Appointment.Domain.Entities;

namespace Appointment.Domain.Interfaces.Mappers;

public interface IAppointmentTimeMapper
{
    AppointmentTime SaveToDomain(AppointmentTimeSave appointmentTimeSave);
    AppointmentTimeCreatedEvent DomainToTimeCreatedEvent(AppointmentTime appointmentTime);
}
