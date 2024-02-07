namespace Appointment.Domain.Options;
public sealed class RabbitMQCredentialsOptions
{
    public required string HostName { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
