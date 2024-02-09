using Appointment.ApplicationService.DataTransferObjects.Appointment;
using Appointment.ApplicationService.Interfaces.Mappers;
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
}
