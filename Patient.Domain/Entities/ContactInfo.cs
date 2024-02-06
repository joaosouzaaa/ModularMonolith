namespace Patient.Domain.Entities;
public sealed class ContactInfo
{
    public int Id { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }

    public int PatientClientId { get; set; }
    public PatientClient PatientClient { get; set; }
}
