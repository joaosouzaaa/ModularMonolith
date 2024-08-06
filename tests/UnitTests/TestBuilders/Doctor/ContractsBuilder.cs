using Doctor.Domain.Contracts;

namespace UnitTests.TestBuilders.Doctor;
public sealed class ContractsBuilder
{
    public static ContractsBuilder NewObject() =>
        new();

    public AppointmentTimeCreatedEvent AppointmentTimeCreatedEventBuild() =>
        new(DateTime.Now,
            12,
            9);
}
