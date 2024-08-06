using Appointment.Domain.Enums;
using ModularMonolith.Common.Extensions;

namespace UnitTests.ExtensionTests;

public sealed class EnumExtensionTests
{
    [Fact]
    public void Description_Equals_AsIntended()
    {
        // A
        const EMessage messageToGetDescription = EMessage.GreaterThan;

        // A
        var messageDescription = messageToGetDescription.Description();

        // A
        Assert.Equal("{0} has to be greater than {1}.", messageDescription);
    }
}
