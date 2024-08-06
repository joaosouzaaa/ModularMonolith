using ModularMonolith.Common.Extensions;

namespace UnitTests.ExtensionTests;

public sealed class StringFormatExtensionTests
{
    [Fact]
    public void FormatTo_SuccessfulScenario_ReturnsFormatedString()
    {
        // A
        const string stringToFormat = "{0} meu nome é {1}";

        // A
        var formattedString = stringToFormat.FormatTo("oi", "joao");

        // A
        Assert.Equal("oi meu nome é joao", formattedString);
    }
}
