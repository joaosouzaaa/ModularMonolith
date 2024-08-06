using Appointment.Domain.Contracts;
using Appointment.Domain.DataTransferObjects.Appointment;
using Appointment.Domain.Entities;
using Appointment.Domain.Interfaces.Mappers;

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
