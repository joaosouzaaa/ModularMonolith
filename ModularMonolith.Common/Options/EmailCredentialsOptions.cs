namespace ModularMonolith.Common.Options;
public sealed class EmailCredentialsOptions
{
    public required int Port { get; set; }
    public required string Host { get; set; }
    public required string Password { get; set; }
    public required string From { get; set; }
}
