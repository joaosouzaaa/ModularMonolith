using Doctor.Domain.Validators;
using UnitTests.TestBuilders.Doctor;

namespace UnitTests.ValidatorTests.Doctor;
public sealed class CertificationValidatorTests
{
    private readonly CertificationValidator _certificationValidator;

    public CertificationValidatorTests()
    {
        _certificationValidator = new CertificationValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var certificationToValidate = CertificationBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _certificationValidator.ValidateAsync(certificationToValidate);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [InlineData("aaa")]
    [InlineData("a")]
    [InlineData("")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
    public async Task ValidateAsync_InvalidLicenseNuber_ReturnsFalse(string licenseNumber)
    {
        // A
        var certificationWithInvalidLicenseNumber = CertificationBuilder.NewObject().WithLicenseNumber(licenseNumber).DomainBuild();

        // A
        var validationResult = await _certificationValidator.ValidateAsync(certificationWithInvalidLicenseNumber);

        // A
        Assert.False(validationResult.IsValid);
    }
}
