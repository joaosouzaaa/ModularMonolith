using Patient.Domain.Validators;
using UnitTests.TestBuilders.Patient;

namespace UnitTests.ValidatorTests.Patient;
public sealed class PatientClientValidatorTests
{
    private readonly PatientClientValidator _patientClientValidator;

    public PatientClientValidatorTests()
    {
        _patientClientValidator = new PatientClientValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var patientClient = PatientClientBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _patientClientValidator.ValidateAsync(patientClient);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Fact]
    public async Task ValidateAsync_InvalidContactInfo_ReturnsFalse()
    {
        // A
        var invalidContactInfo = ContactInfoBuilder.NewObject().WithEmail("test").DomainBuild();
        var patientClientWithInvalidContactInfo = PatientClientBuilder.NewObject().WithContactInfo(invalidContactInfo).DomainBuild();

        // A
        var validationResult = await _patientClientValidator.ValidateAsync(patientClientWithInvalidContactInfo);

        // A
        Assert.False(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidNameParameters))]
    public async Task ValidateAsync_InvalidName_ReturnsFalse(string name)
    {
        // A
        var patientClientWithInvalidName= PatientClientBuilder.NewObject().WithName(name).DomainBuild();

        // A
        var validationResult = await _patientClientValidator.ValidateAsync(patientClientWithInvalidName);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static IEnumerable<object[]> InvalidNameParameters()
    {
        yield return new object[]
        {
            ""
        };

        yield return new object[]
        {
            new string('a', 101)
        };
    }

    [Theory]
    [MemberData(nameof(InvalidAddressParameters))]
    public async Task ValidateAsync_InvalidAddress_ReturnsFalse(string address)
    {
        // A
        var patientClientWithInvalidAddress= PatientClientBuilder.NewObject().WithAddress(address).DomainBuild();

        // A
        var validationResult = await _patientClientValidator.ValidateAsync(patientClientWithInvalidAddress);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static IEnumerable<object[]> InvalidAddressParameters()
    {
        yield return new object[]
        {
            ""
        };

        yield return new object[]
        {
            new string('a', 201)
        };
    }
}
