namespace Patient.Domain.Entities;
public sealed class PatientClient
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }

    public ContactInfo ContactInfo { get; set; }
}
