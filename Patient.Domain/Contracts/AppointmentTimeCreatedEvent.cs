namespace Patient.Domain.Contracts;
public sealed record AppointmentTimeCreatedEvent(DateTime Time,
                                                 int DoctorAttendantId,
                                                 int PatientClientId);
