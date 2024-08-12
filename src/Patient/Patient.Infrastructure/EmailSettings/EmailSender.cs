using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using ModularMonolith.Common.Options;
using Patient.Domain.Arguments;
using Patient.Domain.Interfaces.EmailSettings;

namespace Patient.Infrastructure.EmailSettings;

public sealed class EmailSender(IOptions<EmailCredentialsOptions> emailCredentialsOptions) : IEmailSender
{
    private readonly EmailCredentialsOptions _emailCredentials = emailCredentialsOptions.Value;

    public async Task SendEmailAsync(SendEmailArgument sendEmail, CancellationToken cancellationToken)
    {
        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(_emailCredentials.Host, _emailCredentials.Port, SecureSocketOptions.StartTls, cancellationToken);

        await smtpClient.AuthenticateAsync(_emailCredentials.From, _emailCredentials.Password, cancellationToken);

        var mailMessage = new MimeMessage()
        {
            Subject = sendEmail.Subject,
            To = { MailboxAddress.Parse(sendEmail.To) },
            Body = new TextPart(TextFormat.Text)
            {
                Text = sendEmail.BodyText
            },
            From = { MailboxAddress.Parse(sendEmail.From) }
        };

        await smtpClient.SendAsync(mailMessage, cancellationToken);

        await smtpClient.DisconnectAsync(true, cancellationToken);
    }
}
