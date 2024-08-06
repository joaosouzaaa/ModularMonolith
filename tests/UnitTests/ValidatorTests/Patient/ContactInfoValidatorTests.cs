using FluentValidation;
using Patient.Domain.Validators;
using UnitTests.TestBuilders.Patient;

namespace UnitTests.ValidatorTests.Patient;
public sealed class ContactInfoValidatorTests
{
    private readonly ContactInfoValidator _contactInfoValidator;

    public ContactInfoValidatorTests()
    {
        _contactInfoValidator = new ContactInfoValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var contactInfo = ContactInfoBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _contactInfoValidator.ValidateAsync(contactInfo);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [InlineData("410230")]
    [InlineData("invalid")]
    [InlineData("123456789101")]
    public async Task ValidateAsync_InvalidPhoneNumber_ReturnsFalse(string phoneNumber)
    {
        // A
        var contactInfoWithInvalidPhoneNumber = ContactInfoBuilder.NewObject().WithPhoneNumber(phoneNumber).DomainBuild();

        // A
        var validationResult = await _contactInfoValidator.ValidateAsync(contactInfoWithInvalidPhoneNumber);

        // A
        Assert.False(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidEmailParameters))]
    public async Task ValidateAsync_InvalidEmail_ReturnsFalse(string email)
    {
        // A
        var contactInfoWithInvalidEmail = ContactInfoBuilder.NewObject().WithEmail(email).DomainBuild();

        // A
        var validationResult = await _contactInfoValidator.ValidateAsync(contactInfoWithInvalidEmail);

        // A
        if (validationResult.IsValid)
        {

        }
        Assert.False(validationResult.IsValid);
    }

    public static IEnumerable<object[]> InvalidEmailParameters()
    {
        yield return new object[]
        {
            "a"
        };

        yield return new object[]
        {
            "test"
        };

        yield return new object[]
        {
            "test.com@"
        };

        yield return new object[]
        {
            $"testvalid{new string('a', 100)}@test.com"
        };
    }
}
