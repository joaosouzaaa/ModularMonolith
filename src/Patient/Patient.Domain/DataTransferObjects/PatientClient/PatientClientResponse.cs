using Patient.Domain.DataTransferObjects.ContactInfo;

namespace Patient.Domain.DataTransferObjects.PatientClient;
public sealed class PatientClientResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }

    public required ContactInfoResponse ContactInfo { get; set; }
}
