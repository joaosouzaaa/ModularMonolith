using Doctor.ApplicationService.Mappers;
using Doctor.Domain.Entities;
using UnitTests.TestBuilders.Doctor;

namespace UnitTests.MappersTests.Doctor;
public sealed class ScheduleMapperTests
{
    private readonly ScheduleMapper _scheduleMapper;

    public ScheduleMapperTests()
    {
        _scheduleMapper = new ScheduleMapper();
    }

    [Fact]
    public void DomainListToResponseList_SuccessfulScenario()
    {
        // A
        var scheduleList = new List<Schedule>()
        {
            ScheduleBuilder.NewObject().DomainBuild()
        };

        // A
        var scheduleResponseListResult =_scheduleMapper.DomainListToResponseList(scheduleList);

        // A
        Assert.Equal(scheduleResponseListResult.Count, scheduleList.Count);
    }
}
