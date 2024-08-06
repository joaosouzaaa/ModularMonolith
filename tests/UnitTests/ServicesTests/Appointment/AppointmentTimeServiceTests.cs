using Appointment.ApplicationService.Services;
using Appointment.Domain.Contracts;
using Appointment.Domain.DataTransferObjects.Appointment;
using Appointment.Domain.Entities;
using Appointment.Domain.Interfaces.Mappers;
using Appointment.Domain.Interfaces.Publishers;
using Appointment.Domain.Interfaces.Repositories;
using FluentValidation;
using FluentValidation.Results;
using ModularMonolith.Common.Interfaces.Settings;
using Moq;
using UnitTests.TestBuilders.Appointment;

namespace UnitTests.ServicesTests.Appointment;
public sealed class AppointmentTimeServiceTests
{
    private readonly Mock<IAppointmentTimeRepository> _appointmentTimeRepositoryMock;
    private readonly Mock<IAppointmentPublisher> _appointmentPublisherMock;
    private readonly Mock<IAppointmentTimeMapper> _appointmentTimeMapperMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly Mock<IValidator<AppointmentTime>> _validatorMock;
    private readonly AppointmentTimeService _appointmentTimeService;

    public AppointmentTimeServiceTests()
    {
        _appointmentTimeRepositoryMock = new Mock<IAppointmentTimeRepository>();
        _appointmentPublisherMock = new Mock<IAppointmentPublisher>();
        _appointmentTimeMapperMock = new Mock<IAppointmentTimeMapper>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _validatorMock = new Mock<IValidator<AppointmentTime>>();
        _appointmentTimeService = new AppointmentTimeService(_appointmentTimeRepositoryMock.Object, _appointmentPublisherMock.Object,
            _appointmentTimeMapperMock.Object, _notificationHandlerMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var appointTimeSave = AppointmentTimeBuilder.NewObject().SaveBuild();

        _appointmentTimeRepositoryMock.Setup(a => a.ExistsByTimeAndDoctorAsync(It.IsAny<int>(), It.IsAny<DateTime>()))
            .ReturnsAsync(false);

        var appointmentTime = AppointmentTimeBuilder.NewObject().DomainBuild();
        _appointmentTimeMapperMock.Setup(a => a.SaveToDomain(It.IsAny<AppointmentTimeSave>()))
            .Returns(appointmentTime);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<AppointmentTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _appointmentTimeRepositoryMock.Setup(a => a.AddAsync(It.IsAny<AppointmentTime>()))
            .ReturnsAsync(true);

        var apointmentTimeCreatedEvent = AppointmentTimeBuilder.NewObject().CreatedEventBuild();
        _appointmentTimeMapperMock.Setup(a => a.DomainToTimeCreatedEvent(It.IsAny<AppointmentTime>()))
            .Returns(apointmentTimeCreatedEvent);

        _appointmentPublisherMock.Setup(a => a.PublishAppointmentTimeCreatedMessage(It.IsAny<AppointmentTimeCreatedEvent>()));

        // A
        var addResult = await _appointmentTimeService.AddAsync(appointTimeSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _appointmentTimeRepositoryMock.Verify(a => a.AddAsync(It.IsAny<AppointmentTime>()), Times.Once());
        _appointmentTimeMapperMock.Verify(a => a.DomainToTimeCreatedEvent(It.IsAny<AppointmentTime>()), Times.Once());
        _appointmentPublisherMock.Verify(a => a.PublishAppointmentTimeCreatedMessage(It.IsAny<AppointmentTimeCreatedEvent>()), Times.Once());

        Assert.True(addResult);
    }

    [Fact]
    public async Task AddAsync_TimeAlreadyExists_ReturnsFalse()
    {
        // A
        var appointTimeSave = AppointmentTimeBuilder.NewObject().SaveBuild();

        _appointmentTimeRepositoryMock.Setup(a => a.ExistsByTimeAndDoctorAsync(It.IsAny<int>(), It.IsAny<DateTime>()))
            .ReturnsAsync(true);

        // A
        var addResult = await _appointmentTimeService.AddAsync(appointTimeSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _appointmentTimeMapperMock.Verify(a => a.SaveToDomain(It.IsAny<AppointmentTimeSave>()), Times.Never());
        _appointmentTimeRepositoryMock.Verify(a => a.AddAsync(It.IsAny<AppointmentTime>()), Times.Never());
        _appointmentTimeMapperMock.Verify(a => a.DomainToTimeCreatedEvent(It.IsAny<AppointmentTime>()), Times.Never());
        _appointmentPublisherMock.Verify(a => a.PublishAppointmentTimeCreatedMessage(It.IsAny<AppointmentTimeCreatedEvent>()), Times.Never());

        Assert.False(addResult);
    }

    [Fact]
    public async Task AddAsync_EntityInvalid_ReturnsFalse()
    {
        // A
        var appointTimeSave = AppointmentTimeBuilder.NewObject().SaveBuild();
        
        _appointmentTimeRepositoryMock.Setup(a => a.ExistsByTimeAndDoctorAsync(It.IsAny<int>(), It.IsAny<DateTime>()))
            .ReturnsAsync(false);

        var appointmentTime = AppointmentTimeBuilder.NewObject().DomainBuild();
        _appointmentTimeMapperMock.Setup(a => a.SaveToDomain(It.IsAny<AppointmentTimeSave>()))
            .Returns(appointmentTime);

        var validationFailureList = new List<ValidationFailure>()
        {
            new("ste", "atest"),
            new("ste", "atest"),
            new("ste", "atest")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<AppointmentTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _appointmentTimeRepositoryMock.Setup(a => a.AddAsync(It.IsAny<AppointmentTime>()))
            .ReturnsAsync(true);

        var apointmentTimeCreatedEvent = AppointmentTimeBuilder.NewObject().CreatedEventBuild();
        _appointmentTimeMapperMock.Setup(a => a.DomainToTimeCreatedEvent(It.IsAny<AppointmentTime>()))
            .Returns(apointmentTimeCreatedEvent);

        _appointmentPublisherMock.Setup(a => a.PublishAppointmentTimeCreatedMessage(It.IsAny<AppointmentTimeCreatedEvent>()));

        // A
        var addResult = await _appointmentTimeService.AddAsync(appointTimeSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _appointmentTimeRepositoryMock.Verify(a => a.AddAsync(It.IsAny<AppointmentTime>()), Times.Never());
        _appointmentTimeMapperMock.Verify(a => a.DomainToTimeCreatedEvent(It.IsAny<AppointmentTime>()), Times.Never());
        _appointmentPublisherMock.Verify(a => a.PublishAppointmentTimeCreatedMessage(It.IsAny<AppointmentTimeCreatedEvent>()), Times.Never());

        Assert.False(addResult);
    }
}
