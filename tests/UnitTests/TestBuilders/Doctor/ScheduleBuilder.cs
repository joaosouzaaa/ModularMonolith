using Doctor.Domain.DataTransferObjects.Schedule;
using Doctor.Domain.Entities;

namespace UnitTests.TestBuilders.Doctor;

public sealed class ScheduleBuilder
{
    private readonly DateTime _time = DateTime.Now;

    public static ScheduleBuilder NewObject() =>
        new();

    public Schedule DomainBuild() =>
        new()
        {
            DoctorAttendantId = 123,
            Time = _time
        };

    public ScheduleResponse ResponseBuild() =>
        new(8,
            _time);
}
