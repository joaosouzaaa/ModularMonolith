namespace Doctor.Domain.Entities;
public sealed class Speciality
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public required int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
}
