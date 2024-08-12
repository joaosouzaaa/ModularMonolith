using Patient.Domain.Arguments;

namespace Patient.Domain.Interfaces.EmailSettings;

public interface IEmailSender
{
    Task SendEmailAsync(SendEmailArgument sendEmail, CancellationToken cancellationToken);
}
