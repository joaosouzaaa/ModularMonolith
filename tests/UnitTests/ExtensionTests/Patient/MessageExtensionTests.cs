using Patient.Domain.Enums;
using Patient.Domain.Extensions;

namespace UnitTests.ExtensionTests.Patient;
public sealed class MessageExtensionTests
{
    [Fact]
    public void Description_Equals_AsIntended()
    {
        // A
        var messageToGetDescription = EMessage.InvalidLength;

        // A
        var messageDescription = messageToGetDescription.Description();

        // A
        Assert.Equal("{0} has invalid length. It should be {1}.", messageDescription);
    }
}
