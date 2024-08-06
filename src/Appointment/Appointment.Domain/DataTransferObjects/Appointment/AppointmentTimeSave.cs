namespace Appointment.Domain.DataTransferObjects.Appointment;

public sealed record AppointmentTimeSave(
    DateTime Time,
    int DoctorAttendantId,
    int PatientClientId);
