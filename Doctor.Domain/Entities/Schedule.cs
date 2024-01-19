namespace Doctor.Domain.Entities;
public sealed class Schedule
{
    public int Id { get; set; }
    public required DateTime Time { get; set; }

    public int DoctorId { get; set; }
    public DoctorAttendant Doctor { get; set; }
}
