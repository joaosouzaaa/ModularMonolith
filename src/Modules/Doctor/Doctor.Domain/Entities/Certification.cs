namespace Doctor.Domain.Entities;

public sealed class Certification
{
    public int Id { get; set; }
    public required string LicenseNumber { get; set; }

    public DoctorAttendant DoctorAttendant { get; set; } = null!;
}
