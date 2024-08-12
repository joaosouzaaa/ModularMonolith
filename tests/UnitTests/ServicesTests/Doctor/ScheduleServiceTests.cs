using Doctor.ApplicationService.Services;
using Doctor.Domain.Contracts;
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces.Mappers;
using Doctor.Domain.Interfaces.Repositories;
using Moq;
using UnitTests.TestBuilders.Doctor;

namespace UnitTests.ServicesTests.Doctor;

public sealed class ScheduleServiceTests
{
    private readonly Mock<IScheduleRepository> _scheduleRepositoryMock;
    private readonly Mock<IScheduleMapper> _scheduleMapperMock;
    private readonly ScheduleService _scheduleService;

    public ScheduleServiceTests()
    {
        _scheduleRepositoryMock = new Mock<IScheduleRepository>();
        _scheduleMapperMock = new Mock<IScheduleMapper>();
        _scheduleService = new ScheduleService(
            _scheduleRepositoryMock.Object,
            _scheduleMapperMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_CallsAdd()
    {
        // A
        var appointmentTimeCreatedEvent = ContractsBuilder.NewObject().AppointmentTimeCreatedEventBuild();

        var schedule = ScheduleBuilder.NewObject().DomainBuild();
        _scheduleMapperMock.Setup(s => s.AppointmentTimeCreatedEventToDomain(It.IsAny<AppointmentTimeCreatedEvent>()))
            .Returns(schedule);

        _scheduleRepositoryMock.Setup(s => s.AddAsync(
            It.IsAny<Schedule>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // A
        await _scheduleService.AddAsync(appointmentTimeCreatedEvent, default);

        // A
        _scheduleMapperMock.Verify(s => s.AppointmentTimeCreatedEventToDomain(
            It.IsAny<AppointmentTimeCreatedEvent>()),
            Times.Once());

        _scheduleRepositoryMock.Verify(s => s.AddAsync(
            It.IsAny<Schedule>(),
            It.IsAny<CancellationToken>()),
            Times.Once());
    }
}
