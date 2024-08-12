using Microsoft.Extensions.Options;
using ModularMonolith.Common.Options;
using Patient.Domain.Arguments;
using Patient.Domain.Contracts;
using Patient.Domain.Interfaces.EmailSettings;
using Patient.Domain.Interfaces.Repositories;
using Patient.Domain.Interfaces.Services;

namespace Patient.ApplicationServices.Services;

public sealed class EmailService(
    IPatientClientRepositoryFacade patientClientRepository,
    IEmailSender emailSender,
    IOptions<EmailCredentialsOptions> emailCredentialsOptions)
    : IEmailService
{
    private readonly EmailCredentialsOptions _emailCredentials = emailCredentialsOptions.Value;

    public async Task SendAppointmentEmailAsync(AppointmentTimeCreatedEvent appointmentTimeCreatedEvent, CancellationToken cancellationToken) =>
        await emailSender.SendEmailAsync(
            new SendEmailArgument(
                "Your appointment is booked!",
                (await patientClientRepository.GetEmailByIdAsync(appointmentTimeCreatedEvent.PatientClientId, cancellationToken))!,
                $"Your appointment is booked to {appointmentTimeCreatedEvent.Time.ToString("dd/MM/yyyy HH:mm")}",
                _emailCredentials.From),
            cancellationToken);
}
