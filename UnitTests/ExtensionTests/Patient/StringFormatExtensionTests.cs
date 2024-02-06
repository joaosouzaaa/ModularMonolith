using Patient.Domain.Extensions;

namespace UnitTests.ExtensionTests.Patient;
public sealed class StringFormatExtensionTests
{
    [Fact]
    public void FormatTo_ReturnsFormatedString()
    {
        // A
        var stringToFormat = "{0} meu nome é {1}";

        // A
        var formattedString = stringToFormat.FormatTo("oi", "joao");

        // A
        Assert.Equal("oi meu nome é joao", formattedString);
    }
}
