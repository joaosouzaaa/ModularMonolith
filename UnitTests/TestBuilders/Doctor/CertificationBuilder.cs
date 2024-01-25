using Doctor.ApplicationService.DataTransferObjects.Certification;
using Doctor.Domain.Entities;

namespace UnitTests.TestBuilders.Doctor;
public sealed class CertificationBuilder
{
    private readonly int _id = 123;
    private string _licenseNumber;

    public static CertificationBuilder NewObject() =>
        new();

    public Certification DomainBuild() =>
        new()
        {
            Id = _id,
            LicenseNumber = _licenseNumber
        };

    public CertificationRequest RequestBuild() =>
        new(_licenseNumber);

    public CertificationResponse ResponseBuild() =>
        new()
        {
            Id = _id,
            LicenseNumber = _licenseNumber
        };
}
