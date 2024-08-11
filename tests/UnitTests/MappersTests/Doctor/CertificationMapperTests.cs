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
    public void DomainToResponse_SuccessfulScenario_ReturnsResponseObject()
    {
        // A
        var certification = CertificationBuilder.NewObject().DomainBuild();

        // A
        var certificationResponseResult = _certificationMapper.DomainToResponse(certification);

        // A
        Assert.Equal(certificationResponseResult.Id, certification.Id);
        Assert.Equal(certificationResponseResult.LicenseNumber, certification.LicenseNumber);
    }

    [Fact]
    public void RequestToDomainCreate_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var certificationRequest = CertificationBuilder.NewObject().RequestBuild();

        // A
        var certificationResult = _certificationMapper.RequestToDomainCreate(certificationRequest);

        // A
        Assert.Equal(certificationResult.LicenseNumber, certificationRequest.LicenseNumber);
    }

    [Fact]
    public void RequestToDomainUpdate_SuccessfulScenario_AssignPropertiesSuccessfully()
    {
        // A
        var certificationRequest = CertificationBuilder.NewObject().RequestBuild();
        var certificationResult = CertificationBuilder.NewObject().DomainBuild();

        // A
        _certificationMapper.RequestToDomainUpdate(certificationRequest, certificationResult);

        // A
        Assert.Equal(certificationResult.LicenseNumber, certificationRequest.LicenseNumber);
    }
}
