using Microsoft.Extensions.Options;
using ModularMonolith.Common.Options;
using Moq;
using Patient.ApplicationServices.Services;
using Patient.Domain.Arguments;
using Patient.Domain.Interfaces.EmailSettings;
using Patient.Domain.Interfaces.Repositories;
using UnitTests.TestBuilders.Patient;

namespace UnitTests.ServicesTests.Patient;

public sealed class EmailServiceTests
{
    private readonly Mock<IPatientClientRepositoryFacade> _patientClientRepositoryFacadeMock;
    private readonly Mock<IEmailSender> _emailSenderMock;
    private readonly IOptions<EmailCredentialsOptions> _emailCredentialsOptions;
    private readonly EmailService _emailService;

    public EmailServiceTests()
    {
        _patientClientRepositoryFacadeMock = new Mock<IPatientClientRepositoryFacade>();
        _emailSenderMock = new Mock<IEmailSender>();
        _emailCredentialsOptions = Options.Create(new EmailCredentialsOptions()
        {
            From = "test",
            Host = "test",
            Password = "rando",
            Port = 123
        });
        _emailService = new EmailService(
            _patientClientRepositoryFacadeMock.Object,
            _emailSenderMock.Object,
            _emailCredentialsOptions);
    }

    [Fact]
    public async Task SendAppointmentEmailAsync_SuccessfulScenario()
    {
        // A
        var appointmentTimeCreatedEvent = ContractsBuilder.NewObject().AppointmentTimeCreatedEventBuild();

        const string to = "random";
        _patientClientRepositoryFacadeMock.Setup(p => p.GetEmailByIdAsync(
            It.IsAny<int>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(to);

        // A
        await _emailService.SendAppointmentEmailAsync(appointmentTimeCreatedEvent, default);

        // A
        _patientClientRepositoryFacadeMock.Verify(p => p.GetEmailByIdAsync(
            It.IsAny<int>(),
            It.IsAny<CancellationToken>()),
            Times.Once());

        _emailSenderMock.Verify(e => e.SendEmailAsync(
            It.IsAny<SendEmailArgument>(),
            It.IsAny<CancellationToken>()),
            Times.Once());
    }
}
