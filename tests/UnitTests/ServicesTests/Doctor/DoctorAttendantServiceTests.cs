﻿using Doctor.ApplicationService.Services;
using Doctor.Domain.Arguments;
using Doctor.Domain.DataTransferObjects.DoctorAttendant;
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces.Mappers;
using Doctor.Domain.Interfaces.Repositories;
using Doctor.Domain.Interfaces.Services;
using FluentValidation;
using FluentValidation.Results;
using ModularMonolith.Common.Interfaces.Settings;
using ModularMonolith.Common.Settings.PaginationSettings;
using Moq;
using UnitTests.TestBuilders.Doctor;

namespace UnitTests.ServicesTests.Doctor;

public sealed class DoctorAttendantServiceTests
{
    private readonly Mock<IDoctorAttendantRepository> _doctorAttendantRepositoryMock;
    private readonly Mock<IDoctorAttendantMapper> _doctorAttendantMapperMock;
    private readonly Mock<ISpecialityServiceFacade> _specialityServiceFacadeMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly Mock<IValidator<DoctorAttendant>> _validatorMock;
    private readonly DoctorAttendantService _doctorAttendantService;

    public DoctorAttendantServiceTests()
    {
        _doctorAttendantRepositoryMock = new Mock<IDoctorAttendantRepository>();
        _doctorAttendantMapperMock = new Mock<IDoctorAttendantMapper>();
        _specialityServiceFacadeMock = new Mock<ISpecialityServiceFacade>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _validatorMock = new Mock<IValidator<DoctorAttendant>>();
        _doctorAttendantService = new DoctorAttendantService(
            _doctorAttendantRepositoryMock.Object,
            _doctorAttendantMapperMock.Object,
            _specialityServiceFacadeMock.Object,
            _notificationHandlerMock.Object,
            _validatorMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var specialityIdList = new List<int>()
        {
            1,
            3
        };
        var doctorAttendantSave = DoctorAttendantBuilder.NewObject().WithSpecialityIdList(specialityIdList).SaveBuild();

        var doctorAttendant = DoctorAttendantBuilder.NewObject().DomainBuild();
        _doctorAttendantMapperMock.Setup(d => d.SaveToDomain(It.IsAny<DoctorAttendantSave>()))
            .Returns(doctorAttendant);

        var specialityList = new List<Speciality>()
        {
            SpecialityBuilder.NewObject().DomainBuild(),
            SpecialityBuilder.NewObject().DomainBuild()
        };
        _specialityServiceFacadeMock.Setup(s => s.GetAllByIdListAsync(
            It.IsAny<List<int>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(specialityList);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _doctorAttendantRepositoryMock.Setup(d => d.AddAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // A
        var addResult = await _doctorAttendantService.AddAsync(doctorAttendantSave, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Never());

        _doctorAttendantRepositoryMock.Verify(d => d.AddAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()),
            Times.Once());

        Assert.True(addResult);
    }

    [Fact]
    public async Task AddAsync_SpecialityDoesNotExist_ReturnsFalse()
    {
        // A
        var specialityIdList = new List<int>()
        {
            1,
            3,
            5
        };
        var doctorAttendantSave = DoctorAttendantBuilder.NewObject().WithSpecialityIdList(specialityIdList).SaveBuild();

        var doctorAttendant = DoctorAttendantBuilder.NewObject().DomainBuild();
        _doctorAttendantMapperMock.Setup(d => d.SaveToDomain(It.IsAny<DoctorAttendantSave>()))
            .Returns(doctorAttendant);

        var specialityList = new List<Speciality>()
        {
            SpecialityBuilder.NewObject().DomainBuild()
        };
        _specialityServiceFacadeMock.Setup(s => s.GetAllByIdListAsync(
            It.IsAny<List<int>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(specialityList);

        // A
        var addResult = await _doctorAttendantService.AddAsync(doctorAttendantSave, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Once());

        _validatorMock.Verify(v => v.ValidateAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()),
            Times.Never());

        _doctorAttendantRepositoryMock.Verify(d => d.AddAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()),
            Times.Never());

        Assert.False(addResult);
    }

    [Fact]
    public async Task AddAsync_EntityInvalid_ReturnsFalse()
    {
        // A
        var doctorAttendantSave = DoctorAttendantBuilder.NewObject().SaveBuild();

        var doctorAttendant = DoctorAttendantBuilder.NewObject().DomainBuild();
        _doctorAttendantMapperMock.Setup(d => d.SaveToDomain(It.IsAny<DoctorAttendantSave>()))
            .Returns(doctorAttendant);

        _specialityServiceFacadeMock.Setup(s => s.GetAllByIdListAsync(
            It.IsAny<List<int>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var validationFailureList = new List<ValidationFailure>()
        {
            new("ste", "atest"),
            new("ste", "atest"),
            new("ste", "atest")
        };
        var validationResult = new ValidationResult(validationFailureList);
        _validatorMock.Setup(v => v.ValidateAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var addResult = await _doctorAttendantService.AddAsync(doctorAttendantSave, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Exactly(validationResult.Errors.Count));

        _doctorAttendantRepositoryMock.Verify(d => d.AddAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()),
            Times.Never());

        Assert.False(addResult);
    }

    [Fact]
    public async Task GetAllFilteredAndPaginatedAsync_SuccessfulScenario_RetunsEntityPageList()
    {
        // A
        var doctorGetAllFilterRequest = DoctorAttendantBuilder.NewObject().GetAllFilterRequestBuild();

        var doctorGetAllFilterArgument = DoctorAttendantBuilder.NewObject().GetAllFilterArgumentBuild();
        _doctorAttendantMapperMock.Setup(d => d.FilterRequestToArgumentDomain(It.IsAny<DoctorGetAllFilterRequest>()))
            .Returns(doctorGetAllFilterArgument);

        var doctorAttendantList = new List<DoctorAttendant>()
        {
            DoctorAttendantBuilder.NewObject().DomainBuild(),
            DoctorAttendantBuilder.NewObject().DomainBuild(),
            DoctorAttendantBuilder.NewObject().DomainBuild()
        };
        var doctorAttendantPageList = new PageList<DoctorAttendant>()
        {
            CurrentPage = 1,
            PageSize = 1,
            Result = doctorAttendantList,
            TotalCount = 9,
            TotalPages = 8
        };
        _doctorAttendantRepositoryMock.Setup(d => d.GetAllFilteredAndPaginatedAsync(
            It.IsAny<DoctorGetAllFilterArgument>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(doctorAttendantPageList);

        var doctorAttendantResponseList = new List<DoctorAttendantResponse>()
        {
            DoctorAttendantBuilder.NewObject().ResponseBuild()
        };
        var doctorAttendantResponsePageList = new PageList<DoctorAttendantResponse>()
        {
            CurrentPage = 9,
            PageSize = 8,
            Result = doctorAttendantResponseList,
            TotalCount = 7,
            TotalPages = 5
        };
        _doctorAttendantMapperMock.Setup(d => d.DomainPageListToResponsePageList(It.IsAny<PageList<DoctorAttendant>>()))
            .Returns(doctorAttendantResponsePageList);

        // A
        var doctorAttendantResponsePageListResult = await _doctorAttendantService.GetAllFilteredAndPaginatedAsync(doctorGetAllFilterRequest, default);

        // A
        Assert.Equal(doctorAttendantResponsePageListResult.Result.Count, doctorAttendantResponsePageList.Result.Count);
    }

    [Fact]
    public async Task GetByIdAsync_SuccessfulScenario_ReturnsEntity()
    {
        // A
        var id = 123;

        var doctorAttendant = DoctorAttendantBuilder.NewObject().DomainBuild();
        _doctorAttendantRepositoryMock.Setup(d => d.GetByIdAsync(
            It.IsAny<int>(),
            true,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(doctorAttendant);

        var doctorAttendantResponse = DoctorAttendantBuilder.NewObject().ResponseBuild();
        _doctorAttendantMapperMock.Setup(d => d.DomainToResponse(It.IsAny<DoctorAttendant>()))
            .Returns(doctorAttendantResponse);

        // A
        var doctorAttendantResponseResult = await _doctorAttendantService.GetByIdAsync(id, default);

        // A
        _doctorAttendantMapperMock.Verify(d => d.DomainToResponse(
            It.IsAny<DoctorAttendant>()),
            Times.Once());

        Assert.NotNull(doctorAttendantResponseResult);
    }

    [Fact]
    public async Task GetByIdAsync_EntityDoesNotExist_ReturnsNull()
    {
        // A
        var id = 123;

        _doctorAttendantRepositoryMock.Setup(d => d.GetByIdAsync(
            It.IsAny<int>(),
            true,
            It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<DoctorAttendant?>(null));

        // A
        var doctorAttendantResponseResult = await _doctorAttendantService.GetByIdAsync(id, default);

        // A
        _doctorAttendantMapperMock.Verify(d => d.DomainToResponse(
            It.IsAny<DoctorAttendant>()),
            Times.Never());

        Assert.Null(doctorAttendantResponseResult);
    }

    [Fact]
    public async Task UpdateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var specialityIdList = new List<int>()
        {
            1,
            3,
            7,
            8
        };
        var doctorAttendantUpdate = DoctorAttendantBuilder.NewObject().WithSpecialityIdList(specialityIdList).UpdateBuild();

        var doctorAttendant = DoctorAttendantBuilder.NewObject().DomainBuild();
        _doctorAttendantRepositoryMock.Setup(d => d.GetByIdAsync(
            It.IsAny<int>(),
            false,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(doctorAttendant);

        var specialityList = new List<Speciality>()
        {
            SpecialityBuilder.NewObject().DomainBuild(),
            SpecialityBuilder.NewObject().DomainBuild(),
            SpecialityBuilder.NewObject().DomainBuild(),
            SpecialityBuilder.NewObject().DomainBuild()
        };
        _specialityServiceFacadeMock.Setup(s => s.GetAllByIdListAsync(
            It.IsAny<List<int>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(specialityList);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _doctorAttendantRepositoryMock.Setup(d => d.UpdateAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // A
        var updateResult = await _doctorAttendantService.UpdateAsync(doctorAttendantUpdate, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Never());

        _doctorAttendantRepositoryMock.Verify(d => d.UpdateAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()),
            Times.Once());

        Assert.True(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var doctorAttendantUpdate = DoctorAttendantBuilder.NewObject().UpdateBuild();

        _doctorAttendantRepositoryMock.Setup(d => d.GetByIdAsync(
            It.IsAny<int>(),
            false,
            It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<DoctorAttendant?>(null));

        // A
        var updateResult = await _doctorAttendantService.UpdateAsync(doctorAttendantUpdate, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Once());

        _specialityServiceFacadeMock.Verify(s => s.GetAllByIdListAsync(
            It.IsAny<List<int>>(),
            It.IsAny<CancellationToken>()),
            Times.Never());

        _doctorAttendantMapperMock.Verify(d => d.UpdateToDomain(
            It.IsAny<DoctorAttendantUpdate>(),
            It.IsAny<DoctorAttendant>()),
            Times.Never());

        _validatorMock.Verify(v => v.ValidateAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()),
            Times.Never());

        _doctorAttendantRepositoryMock.Verify(d => d.UpdateAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()),
            Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_SpecialityDoesNotExist_ReturnsFalse()
    {
        // A
        var specialityIdList = new List<int>()
        {
            1,
            3,
            7,
            8
        };
        var doctorAttendantUpdate = DoctorAttendantBuilder.NewObject().WithSpecialityIdList(specialityIdList).UpdateBuild();

        var doctorAttendant = DoctorAttendantBuilder.NewObject().DomainBuild();
        _doctorAttendantRepositoryMock.Setup(d => d.GetByIdAsync(
            It.IsAny<int>(),
            false,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(doctorAttendant);

        var specialityList = new List<Speciality>()
        {
            SpecialityBuilder.NewObject().DomainBuild()
        };
        _specialityServiceFacadeMock.Setup(s => s.GetAllByIdListAsync(
            It.IsAny<List<int>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(specialityList);

        // A
        var updateResult = await _doctorAttendantService.UpdateAsync(doctorAttendantUpdate, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Once());

        _specialityServiceFacadeMock.Verify(s => s.GetAllByIdListAsync(
            It.IsAny<List<int>>(),
            It.IsAny<CancellationToken>()),
            Times.Once());

        _doctorAttendantMapperMock.Verify(d => d.UpdateToDomain(
            It.IsAny<DoctorAttendantUpdate>(),
            It.IsAny<DoctorAttendant>()),
            Times.Never());

        _validatorMock.Verify(v => v.ValidateAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()),
            Times.Never());

        _doctorAttendantRepositoryMock.Verify(d => d.UpdateAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()),
            Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_EntityInvalid_ReturnsFalse()
    {
        // A
        var doctorAttendantUpdate = DoctorAttendantBuilder.NewObject().UpdateBuild();

        var doctorAttendant = DoctorAttendantBuilder.NewObject().DomainBuild();
        _doctorAttendantRepositoryMock.Setup(d => d.GetByIdAsync(
            It.IsAny<int>(),
            false,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(doctorAttendant);

        _specialityServiceFacadeMock.Setup(s => s.GetAllByIdListAsync(
            It.IsAny<List<int>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var validationFailureList = new List<ValidationFailure>()
        {
            new("ste", "atest"),
            new("ste", "atest"),
            new("ste", "atest")
        };
        var validationResult = new ValidationResult(validationFailureList);
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<DoctorAttendant>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var updateResult = await _doctorAttendantService.UpdateAsync(doctorAttendantUpdate, default);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Exactly(validationResult.Errors.Count));

        _doctorAttendantRepositoryMock.Verify(d => d.UpdateAsync(
            It.IsAny<DoctorAttendant>(),
            It.IsAny<CancellationToken>()),
            Times.Never());

        Assert.False(updateResult);
    }
}
