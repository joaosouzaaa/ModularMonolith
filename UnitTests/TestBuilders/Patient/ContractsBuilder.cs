using Patient.Domain.Contracts;

namespace UnitTests.TestBuilders.Patient;
public sealed class ContractsBuilder
{
    public static ContractsBuilder NewObject() =>
        new();

    public AppointmentTimeCreatedEvent AppointmentTimeCreatedEventBuild() =>
        new(DateTime.Now,
            12,
            9);
}
