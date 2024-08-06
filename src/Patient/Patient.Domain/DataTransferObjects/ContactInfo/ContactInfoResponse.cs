namespace Patient.Domain.DataTransferObjects.ContactInfo;
public sealed class ContactInfoResponse
{
    public required int Id { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
}
