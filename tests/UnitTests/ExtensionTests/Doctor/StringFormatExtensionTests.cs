using Doctor.Domain.Extensions;

namespace UnitTests.ExtensionTests.Doctor;
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
