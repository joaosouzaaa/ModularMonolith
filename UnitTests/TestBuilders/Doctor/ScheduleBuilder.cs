using Doctor.ApplicationService.DataTransferObjects.Schedule;
using Doctor.Domain.Entities;

namespace UnitTests.TestBuilders.Doctor;
public sealed class ScheduleBuilder
{
    private readonly int _id = 123;
    private readonly int _doctorId;
    private readonly DateTime _time = DateTime.Now;

    public static ScheduleBuilder NewObject() =>
        new();

    public Schedule DomainBuild() =>
        new()
        {
            DoctorId = _doctorId,
            Time = _time
        };

    public ScheduleResponse ResponseBuild() =>
        new()
        {
            Id = _id,
            Time = _time
        };
}
