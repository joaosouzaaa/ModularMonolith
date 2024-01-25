using Doctor.ApplicationService.Mappers;
using UnitTests.TestBuilders.Doctor;

namespace UnitTests.MappersTests.Doctor;
public sealed class CertificationMapperTests
{
    private readonly CertificationMapper _certificationMapper;

    public CertificationMapperTests()
    {
        _certificationMapper = new CertificationMapper();
    }

    [Fact]
    public void RequestToDomain_SuccessfulScenario()
    {
        // A
        var certificationRequest = CertificationBuilder.NewObject().RequestBuild();

        // A
        var certificationResult = _certificationMapper.RequestToDomain(certificationRequest);

        // A
        Assert.Equal(certificationResult.LicenseNumber, certificationRequest.LicenseNumber);
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario()
    {
        // A
        var certification = CertificationBuilder.NewObject().DomainBuild();

        // A
        var certificationResponseResult = _certificationMapper.DomainToResponse(certification);

        // A
        Assert.Equal(certificationResponseResult.Id, certification.Id);
        Assert.Equal(certificationResponseResult.LicenseNumber, certification.LicenseNumber);
    }
}
