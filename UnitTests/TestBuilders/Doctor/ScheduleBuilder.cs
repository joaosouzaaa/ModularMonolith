using Doctor.ApplicationService.DataTransferObjects.Schedule;
using Doctor.Domain.Entities;

namespace UnitTests.TestBuilders.Doctor;
public sealed class ScheduleBuilder
{
    private readonly int _id = 123;
    private readonly int _doctorAttendantId;
    private readonly DateTime _time = DateTime.Now;

    public static ScheduleBuilder NewObject() =>
        new();

    public Schedule DomainBuild() =>
        new()
        {
            DoctorAttendantId = _doctorAttendantId,
            Time = _time
        };

    public ScheduleResponse ResponseBuild() =>
        new()
        {
            Id = _id,
            Time = _time
        };
}
