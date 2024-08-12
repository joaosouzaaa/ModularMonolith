namespace Patient.Domain.Arguments;

public sealed record SendEmailArgument(
    string Subject,
    string To,
    string BodyText,
    string From);
