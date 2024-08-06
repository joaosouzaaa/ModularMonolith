using Appointment.Domain.Contracts;
using Appointment.Domain.DataTransferObjects.Appointment;
using Appointment.Domain.Entities;

namespace Appointment.Domain.Interfaces.Mappers;

public interface IAppointmentTimeMapper
{
    AppointmentTimeCreatedEvent DomainToTimeCreatedEvent(AppointmentTime appointmentTime);
    AppointmentTime SaveToDomain(AppointmentTimeSave appointmentTimeSave);
}
