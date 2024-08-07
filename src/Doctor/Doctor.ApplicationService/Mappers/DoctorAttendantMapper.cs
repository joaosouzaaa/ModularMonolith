using Doctor.Domain.Arguments;
using Doctor.Domain.DataTransferObjects.DoctorAttendant;
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces.Mappers;
using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.ApplicationService.Mappers;

public sealed class DoctorAttendantMapper(
    ICertificationMapper certificationMapper,
    IScheduleMapper scheduleMapper,
    ISpecialityMapper specialityMapper)
    : IDoctorAttendantMapper
{
    public PageList<DoctorAttendantResponse> DomainPageListToResponsePageList(PageList<DoctorAttendant> doctorAttendantPageList) =>
        new()
        {
            CurrentPage = doctorAttendantPageList.CurrentPage,
            PageSize = doctorAttendantPageList.PageSize,
            Result = doctorAttendantPageList.Result.Select(DomainToResponse).ToList(),
            TotalCount = doctorAttendantPageList.TotalCount,
            TotalPages = doctorAttendantPageList.TotalPages
        };

    public DoctorAttendantResponse DomainToResponse(DoctorAttendant doctorAttendant) =>
        new(doctorAttendant.Id,
            doctorAttendant.Name,
            doctorAttendant.ExperienceYears,
            doctorAttendant.BirthDate,
            certificationMapper.DomainToResponse(doctorAttendant.Certification),
            scheduleMapper.DomainListToResponseList(doctorAttendant.Schedules),
            specialityMapper.DomainListToResponseList(doctorAttendant.Specialities));

    public DoctorGetAllFilterArgument FilterRequestToArgumentDomain(DoctorGetAllFilterRequest doctorGetAllFilterRequest) =>
        new()
        {
            FinalTime = doctorGetAllFilterRequest.FinalTime,
            InitialTime = doctorGetAllFilterRequest.InitialTime,
            PageNumber = doctorGetAllFilterRequest.PageNumber,
            PageSize = doctorGetAllFilterRequest.PageSize,
            SpecialityIds = doctorGetAllFilterRequest.SpecialityIds
        };

    public DoctorAttendant SaveToDomain(DoctorAttendantSave doctorAttendantSave) =>
        new()
        {
            BirthDate = DateOnly.FromDateTime(doctorAttendantSave.BirthDate),
            Certification = certificationMapper.RequestToDomainCreate(doctorAttendantSave.Certification),
            ExperienceYears = doctorAttendantSave.ExperienceYears,
            Name = doctorAttendantSave.Name,
            Schedules = [],
            Specialities = []
        };

    public void UpdateToDomain(DoctorAttendantUpdate doctorAttendantUpdate, DoctorAttendant doctorAttendant)
    {
        doctorAttendant.BirthDate = DateOnly.FromDateTime(doctorAttendantUpdate.BirthDate);
        doctorAttendant.ExperienceYears = doctorAttendantUpdate.ExperienceYears;
        doctorAttendant.Name = doctorAttendantUpdate.Name;

        certificationMapper.RequestToDomainUpdate(doctorAttendantUpdate.Certification, doctorAttendant.Certification);
    }
}
