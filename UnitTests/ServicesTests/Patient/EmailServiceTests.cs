using Microsoft.Extensions.Configuration;
using MimeKit;
using Moq;
using Patient.ApplicationServices.Services;
using Patient.Infrastructure.Interfaces.EmailSettings;
using Patient.Infrastructure.Interfaces.Repositories;
using UnitTests.TestBuilders.Patient;

namespace UnitTests.ServicesTests.Patient;
public sealed class EmailServiceTests
{
    private readonly Mock<IPatientClientRepositoryFacade> _patientClientRepositoryFacadeMock;
    private readonly Mock<IEmailSender> _emailSenderMock;
    private readonly IConfiguration _configuration;
    private readonly EmailService _emailService;

    public EmailServiceTests()
    {
        _patientClientRepositoryFacadeMock = new Mock<IPatientClientRepositoryFacade>();
        _emailSenderMock = new Mock<IEmailSender>();
        var inMemoryCollection = new Dictionary<string, string>()
        {
            {"EmailCredentials:From", "test" }
        };
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemoryCollection!)
            .Build();
        _emailService = new EmailService(_patientClientRepositoryFacadeMock.Object, _emailSenderMock.Object, _configuration);
    }

    [Fact]
    public async Task SendAppointmentEmailAsync_SuccessfulScenario()
    {
        // A
        var appointmentTimeCreatedEvent = ContractsBuilder.NewObject().AppointmentTimeCreatedEventBuild();

        var to = "random";
        _patientClientRepositoryFacadeMock.Setup(p => p.GetEmailByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(to);

        _emailSenderMock.Setup(e => e.SendEmailAsync(It.IsAny<MimeMessage>()));

        // A
        await _emailService.SendAppointmentEmailAsync(appointmentTimeCreatedEvent);

        // A
        _patientClientRepositoryFacadeMock.Verify(p => p.GetEmailByIdAsync(It.IsAny<int>()), Times.Once());
        _emailSenderMock.Verify(e => e.SendEmailAsync(It.IsAny<MimeMessage>()), Times.Once());
    }
}
