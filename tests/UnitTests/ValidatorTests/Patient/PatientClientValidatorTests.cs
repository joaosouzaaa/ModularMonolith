using FluentValidation;
using Moq;
using Patient.ApplicationServices.Validators;
using Patient.Domain.Entities;
using UnitTests.TestBuilders.Patient;

namespace UnitTests.ValidatorTests.Patient;

public sealed class PatientClientValidatorTests
{
    private readonly Mock<IValidator<ContactInfo>> _contactInfoValidatorMock;
    private readonly PatientClientValidator _patientClientValidator;

    public PatientClientValidatorTests()
    {
        _contactInfoValidatorMock = new Mock<IValidator<ContactInfo>>();
        _patientClientValidator = new PatientClientValidator(_contactInfoValidatorMock.Object);
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

    [Theory]
    [MemberData(nameof(InvalidNameParameters))]
    public async Task ValidateAsync_InvalidName_ReturnsFalse(string name)
    {
        // A
        var patientClientWithInvalidName = PatientClientBuilder.NewObject().WithName(name).DomainBuild();

        // A
        var validationResult = await _patientClientValidator.ValidateAsync(patientClientWithInvalidName);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidNameParameters() =>
        new()
        {
            "",
            new string('a', 101)
        };

    [Theory]
    [MemberData(nameof(InvalidAddressParameters))]
    public async Task ValidateAsync_InvalidAddress_ReturnsFalse(string address)
    {
        // A
        var patientClientWithInvalidAddress = PatientClientBuilder.NewObject().WithAddress(address).DomainBuild();

        // A
        var validationResult = await _patientClientValidator.ValidateAsync(patientClientWithInvalidAddress);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidAddressParameters() =>
        new()
        {
            "",
            new string('a', 201)
        };
}
