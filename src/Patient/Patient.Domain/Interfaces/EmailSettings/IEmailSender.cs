using MimeKit;

namespace Patient.Domain.Interfaces.EmailSettings;

public interface IEmailSender
{
    Task SendEmailAsync(MimeMessage mailMessage);
}
