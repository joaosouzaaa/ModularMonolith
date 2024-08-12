using Doctor.ApplicationService.Validators;
using UnitTests.TestBuilders.Doctor;

namespace UnitTests.ValidatorTests.Doctor;

public sealed class SpecialityValidatorTests
{
    private readonly SpecialityValidator _specialityValidator;

    public SpecialityValidatorTests()
    {
        _specialityValidator = new SpecialityValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var specialityToValidate = SpecialityBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _specialityValidator.ValidateAsync(specialityToValidate);

        // A       
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidNameParameters))]
    public async Task ValidateAsync_InvalidName_ReturnsFalse(string name)
    {
        // A
        var specialityWithInvalidName = SpecialityBuilder.NewObject().WithName(name).DomainBuild();

        // A
        var validationResult = await _specialityValidator.ValidateAsync(specialityWithInvalidName);

        // A       
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidNameParameters() =>
        new()
        {
            "",
            new string('a', 101)
        };
}
