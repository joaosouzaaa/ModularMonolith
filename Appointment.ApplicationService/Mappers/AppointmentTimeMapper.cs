using Appointment.ApplicationService.DataTransferObjects.Appointment;
using Appointment.ApplicationService.Interfaces.Mappers;
using Appointment.Domain.Contracts;
using Appointment.Domain.Entities;

namespace Appointment.ApplicationService.Mappers;
public sealed class AppointmentTimeMapper : IAppointmentTimeMapper
{
    public AppointmentTime SaveToDomain(AppointmentTimeSave appointmentTimeSave) =>
        new()
        {
            DoctorAttendantId = appointmentTimeSave.DoctorAttendantId,
            PatientClientId = appointmentTimeSave.PatientClientId,
            Time = appointmentTimeSave.Time
        };

    public AppointmentTimeCreatedEvent DomainToTimeCreatedEvent(AppointmentTime appointmentTime) =>
        new(appointmentTime.Time,
            appointmentTime.DoctorAttendantId,
            appointmentTime.PatientClientId);
}
