using Appointment.ApplicationService.DataTransferObjects.Appointment;
using Appointment.ApplicationService.Interfaces.Mappers;
using Appointment.ApplicationService.Services;
using Appointment.Domain.Entities;
using Appointment.Infrastructure.Interfaces.Publishers;
using Appointment.Infrastructure.Interfaces.Repositories;
using FluentValidation;
using FluentValidation.Results;
using ModularMonolith.Common.Interfaces;
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

        var appointmentTime = AppointmentTimeBuilder.NewObject().DomainBuild();
        _appointmentTimeMapperMock.Setup(a => a.SaveToDomain(It.IsAny<AppointmentTimeSave>()))
            .Returns(appointmentTime);

        var validationResult = new ValidationResult();
        // A

        // A
    }
}
