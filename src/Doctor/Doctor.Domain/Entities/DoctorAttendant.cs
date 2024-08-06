namespace Doctor.Domain.Entities;

public sealed class DoctorAttendant
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int ExperienceYears { get; set; }
    public required DateOnly BirthDate { get; set; }

    public int CertificationId { get; set; }
    public Certification Certification { get; set; } = null!;
    public List<Schedule> Schedules { get; set; } = null!;
    public List<Speciality> Specialities { get; set; } = null!;
}
