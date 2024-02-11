using MimeKit;

namespace Patient.Infrastructure.Interfaces.EmailSettings;
public interface IEmailSender
{
    Task SendEmailAsync(MimeMessage mailMessage);
}
