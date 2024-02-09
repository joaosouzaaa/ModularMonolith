namespace Appointment.ApplicationService.DataTransferObjects.Appointment;
public sealed record AppointmentTimeSave(DateTime Time,
                                     int DoctorAttendantId,
                                     int PatientClientId);
