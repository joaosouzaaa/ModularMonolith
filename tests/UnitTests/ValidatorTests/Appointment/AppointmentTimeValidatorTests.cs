using Appointment.Domain.Validators;
using UnitTests.TestBuilders.Appointment;

namespace UnitTests.ValidatorTests.Appointment;

public sealed class AppointmentTimeValidatorTests
{
    private readonly AppointmentTimeValidator _appointmentTimeValidator;

    public AppointmentTimeValidatorTests()
    {
        _appointmentTimeValidator = new AppointmentTimeValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var appointmentTime = AppointmentTimeBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _appointmentTimeValidator.ValidateAsync(appointmentTime);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidGreaterThan0IdParameters))]
    public async Task ValidateAsync_InvalidDoctorAttendantId_ReturnsFalse(int doctorAttendantId)
    {
        // A
        var appointmentTimeWithInvalidDoctorAttendantId = AppointmentTimeBuilder.NewObject().WithDoctorAttendantId(doctorAttendantId).DomainBuild();

        // A
        var validationResult = await _appointmentTimeValidator.ValidateAsync(appointmentTimeWithInvalidDoctorAttendantId);

        // A
        Assert.False(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidGreaterThan0IdParameters))]
    public async Task ValidateAsync_InvalidPatientClientId_ReturnsFalse(int patientClientId)
    {
        // A
        var appointmentTimeWithInvalidPatientClientId = AppointmentTimeBuilder.NewObject().WithPatientClientId(patientClientId).DomainBuild();

        // A
        var validationResult = await _appointmentTimeValidator.ValidateAsync(appointmentTimeWithInvalidPatientClientId);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<int> InvalidGreaterThan0IdParameters() =>
        new()
        {
            -1,
            -5,
            0
        };

    [Fact]
    public async Task ValidateAsync_InvalidTime_ReturnsFalse()
    {
        // A
        var invalidTime = DateTime.Now.AddDays(-1);
        var appointmentTimeWithInvalidTime = AppointmentTimeBuilder.NewObject().WithTime(invalidTime).DomainBuild();

        // A
        var validationResult = await _appointmentTimeValidator.ValidateAsync(appointmentTimeWithInvalidTime);

        // A
        Assert.False(validationResult.IsValid);
    }
}
