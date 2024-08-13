using FluentValidation;
using FluentValidation.Results;
using ModularMonolith.Common.Interfaces.Settings;
using Moq;
using Patient.ApplicationServices.Services;
using Patient.Domain.DataTransferObjects.PatientClient;
using Patient.Domain.Entities;
using Patient.Domain.Interfaces.Mappers;
using Patient.Domain.Interfaces.Repositories;
using UnitTests.TestBuilders.Patient;

namespace UnitTests.ServicesTests.Patient;

public sealed class PatientClientServiceTests
{
    private readonly Mock<IPatientClientRepository> _patientClientRepositoryMock;
    private readonly Mock<IPatientClientMapper> _patientClientMapperMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly Mock<IValidator<PatientClient>> _validatorMock;
    private readonly PatientClientService _patientClientService;

    public PatientClientServiceTests()
    {
        _patientClientRepositoryMock = new Mock<IPatientClientRepository>();
        _patientClientMapperMock = new Mock<IPatientClientMapper>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _validatorMock = new Mock<IValidator<PatientClient>>();
        _patientClientService = new PatientClientService(
            _patientClientRepositoryMock.Object,
            _patientClientMapperMock.Object,
            _notificationHandlerMock.Object,
            _validatorMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var patientClientSave = PatientClientBuilder.NewObject().SaveBuild();

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(
            It.IsAny<PatientClient>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _patientClientRepositoryMock.Setup(p => p.AddAsync(
            It.IsAny<PatientClient>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // A
        var addResult = await _patientClientService.AddAsync(patientClientSave, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Never());

        _patientClientRepositoryMock.Verify(p => p.AddAsync(
            It.IsAny<PatientClient>(),
            It.IsAny<CancellationToken>()),
            Times.Once());

        Assert.True(addResult);
    }

    [Fact]
    public async Task AddAsync_EntityInvalid_ReturnsFalse()
    {
        // A
        var patientClientSave = PatientClientBuilder.NewObject().SaveBuild();

        var validationFailureList = new List<ValidationFailure>()
        {
            new("test", "a")
        };
        var validationResult = new ValidationResult(validationFailureList);
        _validatorMock.Setup(v => v.ValidateAsync(
            It.IsAny<PatientClient>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var addResult = await _patientClientService.AddAsync(patientClientSave, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Exactly(validationResult.Errors.Count));

        _patientClientRepositoryMock.Verify(p => p.AddAsync(
            It.IsAny<PatientClient>(),
            It.IsAny<CancellationToken>()),
            Times.Never());

        Assert.False(addResult);
    }

    [Fact]
    public async Task GetAllAsync_SuccessfulScenario_ReturnsResponseList()
    {
        // A
        var patientClientList = new List<PatientClient>()
        {
            PatientClientBuilder.NewObject().DomainBuild(),
            PatientClientBuilder.NewObject().DomainBuild(),
            PatientClientBuilder.NewObject().DomainBuild()
        };
        _patientClientRepositoryMock.Setup(p => p.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(patientClientList);

        var patientClientResponseList = new List<PatientClientResponse>()
        {
            PatientClientBuilder.NewObject().ResponseBuild(),
            PatientClientBuilder.NewObject().ResponseBuild(),
            PatientClientBuilder.NewObject().ResponseBuild(),
            PatientClientBuilder.NewObject().ResponseBuild()
        };
        _patientClientMapperMock.Setup(p => p.DomainListToResponseList(It.IsAny<List<PatientClient>>()))
            .Returns(patientClientResponseList);

        // A
        var patientClientResponseListResult = await _patientClientService.GetAllAsync(default);

        // A
        Assert.Equal(patientClientResponseListResult.Count, patientClientResponseList.Count);
    }

    [Fact]
    public async Task GetByIdAsync_SuccessfulScenario_ReturnsEntity()
    {
        // A
        var id = 123;

        var patientClient = PatientClientBuilder.NewObject().DomainBuild();
        _patientClientRepositoryMock.Setup(p => p.GetByIdAsync(
            It.IsAny<int>(),
            true,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(patientClient);

        var patientClientResponse = PatientClientBuilder.NewObject().ResponseBuild();
        _patientClientMapperMock.Setup(p => p.DomainToResponse(It.IsAny<PatientClient>()))
            .Returns(patientClientResponse);

        // A
        var patientClientResponseResult = await _patientClientService.GetByIdAsync(id, default);

        // A
        _patientClientMapperMock.Verify(p => p.DomainToResponse(
            It.IsAny<PatientClient>()),
            Times.Once());

        Assert.NotNull(patientClientResponseResult);
    }

    [Fact]
    public async Task GetByIdAsync_EntityDoesNotExist_ReturnsNull()
    {
        // A
        var id = 123;

        _patientClientRepositoryMock.Setup(p => p.GetByIdAsync(
            It.IsAny<int>(),
            true,
            It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<PatientClient?>(null));

        // A
        var patientClientResponseResult = await _patientClientService.GetByIdAsync(id, default);

        // A
        _patientClientMapperMock.Verify(p => p.DomainToResponse(
            It.IsAny<PatientClient>()),
            Times.Never());

        Assert.Null(patientClientResponseResult);
    }

    [Fact]
    public async Task UpdateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var patientClientUpdate = PatientClientBuilder.NewObject().UpdateBuild();

        var patientClient = PatientClientBuilder.NewObject().DomainBuild();
        _patientClientRepositoryMock.Setup(p => p.GetByIdAsync(
            It.IsAny<int>(),
            false,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(patientClient);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(
            It.IsAny<PatientClient>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _patientClientRepositoryMock.Setup(p => p.UpdateAsync(
            It.IsAny<PatientClient>(),
            default))
            .ReturnsAsync(true);

        // A
        var updateResult = await _patientClientService.UpdateAsync(patientClientUpdate, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Never());

        _patientClientRepositoryMock.Verify(p => p.UpdateAsync(
            It.IsAny<PatientClient>(),
            It.IsAny<CancellationToken>()),
            Times.Once());

        Assert.True(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var patientClientUpdate = PatientClientBuilder.NewObject().UpdateBuild();

        _patientClientRepositoryMock.Setup(p => p.GetByIdAsync(
            It.IsAny<int>(),
            false,
            It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<PatientClient?>(null));

        // A
        var updateResult = await _patientClientService.UpdateAsync(patientClientUpdate, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Once());

        _patientClientMapperMock.Verify(p => p.UpdateToDomain(
            It.IsAny<PatientClientUpdate>(),
            It.IsAny<PatientClient>()),
            Times.Never());

        _validatorMock.Verify(v => v.ValidateAsync(
            It.IsAny<PatientClient>(),
            It.IsAny<CancellationToken>()),
            Times.Never());

        _patientClientRepositoryMock.Verify(p => p.UpdateAsync(
            It.IsAny<PatientClient>(),
            It.IsAny<CancellationToken>()),
            Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_EntityInvalid_ReturnsFalse()
    {
        // A
        var patientClientUpdate = PatientClientBuilder.NewObject().UpdateBuild();

        var patientClient = PatientClientBuilder.NewObject().DomainBuild();
        _patientClientRepositoryMock.Setup(p => p.GetByIdAsync(
            It.IsAny<int>(),
            false,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(patientClient);

        var validationFailureList = new List<ValidationFailure>()
        {
            new("test", "a"),
            new("test", "a"),
            new("test", "a")
        };
        var validationResult = new ValidationResult(validationFailureList);
        _validatorMock.Setup(v => v.ValidateAsync(
            It.IsAny<PatientClient>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var updateResult = await _patientClientService.UpdateAsync(patientClientUpdate, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Exactly(validationResult.Errors.Count));

        _patientClientRepositoryMock.Verify(p => p.UpdateAsync(
            It.IsAny<PatientClient>(),
            It.IsAny<CancellationToken>()),
            Times.Never());

        Assert.False(updateResult);
    }
}
