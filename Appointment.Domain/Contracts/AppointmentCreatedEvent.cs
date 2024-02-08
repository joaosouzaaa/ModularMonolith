namespace Appointment.Domain.Contracts;
public sealed record AppointmentCreatedEvent(DateTime Time,
                                             int DoctorAttendantId,
                                             int PatientClientId);
