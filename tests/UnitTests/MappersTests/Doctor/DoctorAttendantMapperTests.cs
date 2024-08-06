using Doctor.ApplicationService.Mappers;
using Doctor.Domain.DataTransferObjects.Certification;
using Doctor.Domain.DataTransferObjects.Schedule;
using Doctor.Domain.DataTransferObjects.Speciality;
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces.Mappers;
using ModularMonolith.Common.Settings.PaginationSettings;
using Moq;
using UnitTests.TestBuilders.Doctor;

namespace UnitTests.MappersTests.Doctor;
public sealed class DoctorAttendantMapperTests
{
    private readonly Mock<ICertificationMapper> _certificationMapperMock;
    private readonly Mock<IScheduleMapper> _scheduleMapperMock;
    private readonly Mock<ISpecialityMapper> _specialityMapperMock;
    private readonly DoctorAttendantMapper _doctorAttendantMapper;

    public DoctorAttendantMapperTests()
    {
        _certificationMapperMock = new Mock<ICertificationMapper>();
        _scheduleMapperMock = new Mock<IScheduleMapper>();
        _specialityMapperMock = new Mock<ISpecialityMapper>();
        _doctorAttendantMapper = new DoctorAttendantMapper(_certificationMapperMock.Object, _scheduleMapperMock.Object, _specialityMapperMock.Object);
    }

    [Fact]
    public void SaveToDomain_SuccessfulScenario()
    {
        // A
        var doctorAttendantSave = DoctorAttendantBuilder.NewObject().SaveBuild();

        var certification = CertificationBuilder.NewObject().DomainBuild();
        _certificationMapperMock.Setup(c => c.RequestToDomainCreate(It.IsAny<CertificationRequest>()))
            .Returns(certification);

        // A
        var doctorAttendantResult = _doctorAttendantMapper.SaveToDomain(doctorAttendantSave);

        // A
        Assert.Equal(doctorAttendantResult.BirthDate, DateOnly.FromDateTime(doctorAttendantSave.BirthDate));
        Assert.NotNull(doctorAttendantResult.Certification);
        Assert.Equal(doctorAttendantResult.ExperienceYears, doctorAttendantSave.ExperienceYears);
        Assert.Equal(doctorAttendantResult.Name, doctorAttendantSave.Name);
    }

    [Fact]
    public void UpdateToDomain_SuccessfulScenario()
    {
        // A
        var doctorAttendantUpdate = DoctorAttendantBuilder.NewObject().UpdateBuild();
        var doctorAttendantResult = DoctorAttendantBuilder.NewObject().DomainBuild();

        _certificationMapperMock.Setup(c => c.RequestToDomainUpdate(It.IsAny<CertificationRequest>(), It.IsAny<Certification>()));

        // A
        _doctorAttendantMapper.UpdateToDomain(doctorAttendantUpdate, doctorAttendantResult);

        // A
        Assert.Equal(doctorAttendantResult.BirthDate, DateOnly.FromDateTime(doctorAttendantUpdate.BirthDate));
        Assert.Equal(doctorAttendantResult.ExperienceYears, doctorAttendantUpdate.ExperienceYears);
        Assert.Equal(doctorAttendantResult.Name, doctorAttendantUpdate.Name);
        Assert.NotNull(doctorAttendantResult.Certification);
    }

    [Fact]
    public void FilterRequestToArgumentDomain_SuccessfulScenario()
    {
        // A
        var specialityIdList = new List<int>()
        {
            1,
            2
        };
        var doctorGetAllFilterRequest = DoctorAttendantBuilder.NewObject().WithSpecialityIdList(specialityIdList).GetAllFilterRequestBuild();

        // A
        var doctorGetAllFilterArgumentResult = _doctorAttendantMapper.FilterRequestToArgumentDomain(doctorGetAllFilterRequest);

        // A
        Assert.Equal(doctorGetAllFilterArgumentResult.FinalTime, doctorGetAllFilterRequest.FinalTime);
        Assert.Equal(doctorGetAllFilterArgumentResult.InitialTime, doctorGetAllFilterRequest.InitialTime);
        Assert.Equal(doctorGetAllFilterArgumentResult.PageNumber, doctorGetAllFilterRequest.PageNumber);
        Assert.Equal(doctorGetAllFilterArgumentResult.PageSize, doctorGetAllFilterRequest.PageSize);
        Assert.Equal(doctorGetAllFilterArgumentResult.SpecialityIds.Count, doctorGetAllFilterRequest.SpecialityIds.Count);
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario()
    {
        // A
        var scheduleList = new List<Schedule>()
        {
            ScheduleBuilder.NewObject().DomainBuild(),
            ScheduleBuilder.NewObject().DomainBuild()
        };
        var specialityList = new List<Speciality>()
        {
            SpecialityBuilder.NewObject().DomainBuild(),
            SpecialityBuilder.NewObject().DomainBuild(),
            SpecialityBuilder.NewObject().DomainBuild()
        };

        var doctorAttendat = DoctorAttendantBuilder.NewObject().WithScheduleList(scheduleList).WithSpecialityList(specialityList).DomainBuild();

        var certificationResponse = CertificationBuilder.NewObject().ResponseBuild();
        _certificationMapperMock.Setup(c => c.DomainToResponse(It.IsAny<Certification>()))
            .Returns(certificationResponse);

        var scheduleResponseList = new List<ScheduleResponse>()
        {
            ScheduleBuilder.NewObject().ResponseBuild()
        };
        _scheduleMapperMock.Setup(s => s.DomainListToResponseList(It.IsAny<List<Schedule>>()))
            .Returns(scheduleResponseList);

        var specialityResponseList = new List<SpecialityResponse>()
        {
            SpecialityBuilder.NewObject().ResponseBuild()
        };
        _specialityMapperMock.Setup(s => s.DomainLisToResponseList(It.IsAny<List<Speciality>>()))
            .Returns(specialityResponseList);

        // A
        var doctorAttendantResponseResult = _doctorAttendantMapper.DomainToResponse(doctorAttendat);

        // A
        Assert.Equal(doctorAttendantResponseResult.BirthDate, doctorAttendat.BirthDate);
        Assert.NotNull(doctorAttendantResponseResult.Certification);
        Assert.Equal(doctorAttendantResponseResult.ExperienceYears, doctorAttendat.ExperienceYears);
        Assert.Equal(doctorAttendantResponseResult.Id, doctorAttendat.Id);
        Assert.Equal(doctorAttendantResponseResult.Name, doctorAttendat.Name);
        Assert.Equal(doctorAttendantResponseResult.Schedules.Count, scheduleResponseList.Count);
        Assert.Equal(doctorAttendantResponseResult.Specialities.Count, specialityResponseList.Count);
    }

    [Fact]
    public void DomainPageListToResponsePageList_SuccessfulScenario()
    {
        // A
        var scheduleList = new List<Schedule>()
        {
            ScheduleBuilder.NewObject().DomainBuild(),
            ScheduleBuilder.NewObject().DomainBuild()
        };
        var specialityList = new List<Speciality>()
        {
            SpecialityBuilder.NewObject().DomainBuild(),
            SpecialityBuilder.NewObject().DomainBuild(),
            SpecialityBuilder.NewObject().DomainBuild()
        };

        var doctorAttendat = DoctorAttendantBuilder.NewObject().WithScheduleList(scheduleList).WithSpecialityList(specialityList).DomainBuild();
        var doctorAttendantList = new List<DoctorAttendant>()
        {
            doctorAttendat,
            doctorAttendat
        };
        var doctorAttendantPageList = new PageList<DoctorAttendant>()
        {
            CurrentPage = 123,
            PageSize = 1,
            Result = doctorAttendantList,
            TotalCount = 1,
            TotalPages = 8
        };

        var certificationResponse = CertificationBuilder.NewObject().ResponseBuild();
        _certificationMapperMock.SetupSequence(c => c.DomainToResponse(It.IsAny<Certification>()))
            .Returns(certificationResponse)
            .Returns(certificationResponse);

        var scheduleResponseList = new List<ScheduleResponse>()
        {
            ScheduleBuilder.NewObject().ResponseBuild()
        };
        _scheduleMapperMock.SetupSequence(s => s.DomainListToResponseList(It.IsAny<List<Schedule>>()))
            .Returns(scheduleResponseList)
            .Returns(scheduleResponseList);

        var specialityResponseList = new List<SpecialityResponse>()
        {
            SpecialityBuilder.NewObject().ResponseBuild()
        };
        _specialityMapperMock.SetupSequence(s => s.DomainLisToResponseList(It.IsAny<List<Speciality>>()))
            .Returns(specialityResponseList)
            .Returns(specialityResponseList);

        // A
        var doctorAttendantResponsePageListResult = _doctorAttendantMapper.DomainPageListToResponsePageList(doctorAttendantPageList);

        // A
        Assert.Equal(doctorAttendantResponsePageListResult.CurrentPage, doctorAttendantPageList.CurrentPage);
        Assert.Equal(doctorAttendantResponsePageListResult.PageSize, doctorAttendantPageList.PageSize);
        Assert.Equal(doctorAttendantResponsePageListResult.Result.Count, doctorAttendantPageList.Result.Count);
        Assert.Equal(doctorAttendantResponsePageListResult.TotalCount, doctorAttendantPageList.TotalCount);
        Assert.Equal(doctorAttendantResponsePageListResult.TotalPages, doctorAttendantPageList.TotalPages);
    }
}
