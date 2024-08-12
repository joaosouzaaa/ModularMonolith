namespace ModularMonolith.Common.Options;

public sealed class EmailCredentialsOptions
{
    public required int Port { get; init; }
    public required string Host { get; init; }
    public required string Password { get; init; }
    public required string From { get; init; }
}
