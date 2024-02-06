namespace Patient.Domain.Extensions;
public static class StringFormatExtension
{
    public static string FormatTo(this string message, params object[] args) =>
        string.Format(message, args);
}
