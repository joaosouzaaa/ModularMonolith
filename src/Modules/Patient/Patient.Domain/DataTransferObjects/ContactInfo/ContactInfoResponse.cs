namespace Patient.Domain.DataTransferObjects.ContactInfo;

public sealed record ContactInfoResponse(
    int Id,
    string PhoneNumber,
    string Email);
