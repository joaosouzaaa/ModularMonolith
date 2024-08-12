namespace ModularMonolith.Common.Options;

public sealed class RabbitMQCredentialsOptions
{
    public required string HostName { get; init; }
    public required int Port { get; init; }
    public required string UserName { get; init; }
    public required string Password { get; init; }
}
