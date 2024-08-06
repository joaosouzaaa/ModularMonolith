using Doctor.ApplicationService.Services;
using Doctor.Domain.DataTransferObjects.Speciality;
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces.Mappers;
using Doctor.Domain.Interfaces.Repositories;
using FluentValidation;
using FluentValidation.Results;
using ModularMonolith.Common.Interfaces.Settings;
using Moq;
using UnitTests.TestBuilders.Doctor;

namespace UnitTests.ServicesTests.Doctor;
public sealed class SpecialityServiceTests
{
    private readonly Mock<ISpecialityRepository> _specialityRepositoryMock;
    private readonly Mock<ISpecialityMapper> _specialityMapperMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly Mock<IValidator<Speciality>> _validatorMock;
    private readonly SpecialityService _specialityService;

    public SpecialityServiceTests()
    {
        _specialityRepositoryMock = new Mock<ISpecialityRepository>();
        _specialityMapperMock = new Mock<ISpecialityMapper>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _validatorMock = new Mock<IValidator<Speciality>>();
        _specialityService = new SpecialityService(_specialityRepositoryMock.Object, _specialityMapperMock.Object, _notificationHandlerMock.Object,
            _validatorMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var specialitySave = SpecialityBuilder.NewObject().SaveBuild();

        var speciality = SpecialityBuilder.NewObject().DomainBuild();
        _specialityMapperMock.Setup(s => s.SaveToDomain(It.IsAny<SpecialitySave>()))
            .Returns(speciality);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Speciality>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _specialityRepositoryMock.Setup(s => s.AddAsync(It.IsAny<Speciality>()))
            .ReturnsAsync(true);

        // A
        var addResult = await _specialityService.AddAsync(specialitySave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _specialityRepositoryMock.Verify(s => s.AddAsync(It.IsAny<Speciality>()), Times.Once());

        Assert.True(addResult);
    }

    [Fact]
    public async Task AddAsync_EntityInvalid_ReturnsFalse()
    {
        // A
        var specialitySave = SpecialityBuilder.NewObject().SaveBuild();

        var speciality = SpecialityBuilder.NewObject().DomainBuild();
        _specialityMapperMock.Setup(s => s.SaveToDomain(It.IsAny<SpecialitySave>()))
            .Returns(speciality);

        var validationFailureList = new List<ValidationFailure>()
        {
            new ("test", "error")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Speciality>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var addResult = await _specialityService.AddAsync(specialitySave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _specialityRepositoryMock.Verify(s => s.AddAsync(It.IsAny<Speciality>()), Times.Never());

        Assert.False(addResult);
    }

    [Fact]
    public async Task DeleteAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var id = 123;

        _specialityRepositoryMock.Setup(s => s.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        _specialityRepositoryMock.Setup(s => s.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(true);
        // A
        var deleteResult = await _specialityService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _specialityRepositoryMock.Verify(s => s.DeleteAsync(It.IsAny<int>()), Times.Once());

        Assert.True(deleteResult);
    }

    [Fact]
    public async Task DeleteAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var id = 123;

        _specialityRepositoryMock.Setup(s => s.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        // A
        var deleteResult = await _specialityService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _specialityRepositoryMock.Verify(s => s.DeleteAsync(It.IsAny<int>()), Times.Never());

        Assert.False(deleteResult);
    }

    [Fact]
    public async Task GetAllAsync_SuccessfulScenario_ReturnsEntityList()
    {
        // A
        var specialityList = new List<Speciality>()
        {
            SpecialityBuilder.NewObject().DomainBuild()
        };
        _specialityRepositoryMock.Setup(s => s.GetAllAsync())
            .ReturnsAsync(specialityList);

        var specialityResponseList = new List<SpecialityResponse>()
        {
            SpecialityBuilder.NewObject().ResponseBuild(),
            SpecialityBuilder.NewObject().ResponseBuild(),
            SpecialityBuilder.NewObject().ResponseBuild(),
            SpecialityBuilder.NewObject().ResponseBuild()
        };
        _specialityMapperMock.Setup(s => s.DomainLisToResponseList(It.IsAny<List<Speciality>>()))
            .Returns(specialityResponseList);

        // A
        var specialityResponseListResult = await _specialityService.GetAllAsync();

        // A
        Assert.Equal(specialityResponseListResult.Count, specialityResponseList.Count);
    }

    [Fact]
    public async Task GetByIdReturnsDomainObjectAsync_SuccessfulScenario_ReturnsExpectedEntity()
    {
        // A
        var id = 123;

        _specialityRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>()))
            .Returns(Task.FromResult<Speciality?>(null));

        // A
        var specialityResult = await _specialityService.GetByIdReturnsDomainObjectAsync(id);

        // A
        Assert.Null(specialityResult);
    }
}
