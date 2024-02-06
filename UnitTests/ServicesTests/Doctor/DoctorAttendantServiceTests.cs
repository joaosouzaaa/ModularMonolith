using Doctor.ApplicationService.Interfaces.Mappers;
using Doctor.ApplicationService.Interfaces.Services;
using Doctor.ApplicationService.Services;
using Doctor.Domain.Entities;
using Doctor.Infrasctructure.Interfaces.Repositories;
using FluentValidation;
using ModularMonolith.Common.Interfaces;
using Moq;
using UnitTests.TestBuilders.Doctor;

namespace UnitTests.ServicesTests.Doctor;
public sealed class DoctorAttendantServiceTests
{
    private readonly Mock<IDoctorAttendantRepository> _doctorAttendantRepositoryMock;
    private readonly Mock<IDoctorAttendantMapper > _doctorAttendantMapperMock;
    private readonly Mock<INotificationHandler > _notificationHandlerMock;
    private readonly Mock<IValidator<DoctorAttendant> > _validatorMock;
    private readonly DoctorAttendantService _doctorAttendantService;

    public DoctorAttendantServiceTests()
    {
        _doctorAttendantRepositoryMock = new Mock<IDoctorAttendantRepository>();
        _doctorAttendantMapperMock = new Mock<IDoctorAttendantMapper>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _validatorMock = new Mock<IValidator<DoctorAttendant>>();
        _doctorAttendantService = new DoctorAttendantService(_doctorAttendantRepositoryMock.Object, _doctorAttendantMapperMock.Object, _notificationHandlerMock.Object,
            _validatorMock.Object);
    }

    public async Task AddAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var doctorAttendantSave = DoctorAttendantBuilder.NewObject().SaveBuild();
        // A

        // A
    }
}
