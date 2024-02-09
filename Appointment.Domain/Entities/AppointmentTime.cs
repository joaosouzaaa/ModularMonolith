namespace Appointment.Domain.Entities;
public sealed class AppointmentTime
{
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public required int DoctorAttendantId {  get; set; }
    public required int PatientClientId { get; set; }
}
