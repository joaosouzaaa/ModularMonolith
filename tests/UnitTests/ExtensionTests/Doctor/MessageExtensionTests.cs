using Doctor.Domain.Enums;
using Doctor.Domain.Extensions;

namespace UnitTests.ExtensionTests.Doctor;
public sealed class MessageExtensionTests
{
    [Fact]
    public void Description_Equals_AsIntended()
    {
        // A
        var messageToGetDescription = EMessage.Required;

        // A
        var messageDescription = messageToGetDescription.Description();

        // A
        Assert.Equal("{0} needs to be filled.", messageDescription);
    }
}
