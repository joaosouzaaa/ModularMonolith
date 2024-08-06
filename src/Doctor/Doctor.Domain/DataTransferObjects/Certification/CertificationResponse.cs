namespace Doctor.Domain.DataTransferObjects.Certification;

public sealed class CertificationResponse
{
    public required int Id { get; set; }
    public required string LicenseNumber { get; set; }
}
