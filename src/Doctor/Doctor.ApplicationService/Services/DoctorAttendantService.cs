using Doctor.ApplicationService.Services.BaseServices;
using Doctor.Domain.DataTransferObjects.DoctorAttendant;
using Doctor.Domain.Entities;
using Doctor.Domain.Enums;
using Doctor.Domain.Interfaces.Mappers;
using Doctor.Domain.Interfaces.Repositories;
using Doctor.Domain.Interfaces.Services;
using FluentValidation;
using ModularMonolith.Common.Extensions;
using ModularMonolith.Common.Interfaces.Settings;
using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.ApplicationService.Services;

public sealed class DoctorAttendantService : BaseService<DoctorAttendant>, IDoctorAttendantService
{
    private readonly IDoctorAttendantRepository _doctorAttendantRepository;
    private readonly IDoctorAttendantMapper _doctorAttendantMapper;
    private readonly ISpecialityServiceFacade _specialityServiceFacade;

    public DoctorAttendantService(
        IDoctorAttendantRepository doctorAttendantRepository,
        IDoctorAttendantMapper doctorAttendantMapper,
        ISpecialityServiceFacade specialityServiceFacade,
        INotificationHandler notificationHandler,
        IValidator<DoctorAttendant> validator)
        : base(
            notificationHandler,
            validator)
    {
        _doctorAttendantRepository = doctorAttendantRepository;
        _doctorAttendantMapper = doctorAttendantMapper;
        _specialityServiceFacade = specialityServiceFacade;
    }

    public async Task<bool> AddAsync(DoctorAttendantSave doctorAttendantSave, CancellationToken cancellationToken)
    {
        var doctorAttendant = _doctorAttendantMapper.SaveToDomain(doctorAttendantSave);

        if (!await AddSpecialityRelationshipAsync(doctorAttendantSave.SpecialityIds, doctorAttendant, cancellationToken))
        {
            return false;
        }

        if (!await IsValidAsync(doctorAttendant, cancellationToken))
        {
            return false;
        }

        return await _doctorAttendantRepository.AddAsync(doctorAttendant, cancellationToken);
    }

    public async Task<bool> UpdateAsync(DoctorAttendantUpdate doctorAttendantUpdate, CancellationToken cancellationToken)
    {
        var doctorAttendant = await _doctorAttendantRepository.GetByIdAsync(doctorAttendantUpdate.Id, false, cancellationToken);

        if (doctorAttendant is null)
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Doctor Attendant"));

            return false;
        }

        if (!await AddSpecialityRelationshipAsync(doctorAttendantUpdate.SpecialityIds, doctorAttendant, cancellationToken))
        {
            return false;
        }

        _doctorAttendantMapper.UpdateToDomain(doctorAttendantUpdate, doctorAttendant);

        if (!await IsValidAsync(doctorAttendant, cancellationToken))
        {
            return false;
        }

        return await _doctorAttendantRepository.UpdateAsync(doctorAttendant, cancellationToken);
    }

    public async Task<PageList<DoctorAttendantResponse>> GetAllFilteredAndPaginatedAsync(DoctorGetAllFilterRequest filterRequest, CancellationToken cancellationToken)
    {
        var getAllFilterArgument = _doctorAttendantMapper.FilterRequestToArgumentDomain(filterRequest);

        var doctorPageList = await _doctorAttendantRepository.GetAllFilteredAndPaginatedAsync(getAllFilterArgument, cancellationToken);

        return _doctorAttendantMapper.DomainPageListToResponsePageList(doctorPageList);
    }

    public async Task<DoctorAttendantResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var doctorAttendant = await _doctorAttendantRepository.GetByIdAsync(id, true, cancellationToken);

        if (doctorAttendant is null)
        {
            return null;
        }

        return _doctorAttendantMapper.DomainToResponse(doctorAttendant);
    }

    private async Task<bool> AddSpecialityRelationshipAsync(List<int> specialityRequestIdList, DoctorAttendant doctorAttendant, CancellationToken cancellationToken)
    {
        var specialityList = await _specialityServiceFacade.GetAllByIdListAsync(specialityRequestIdList, cancellationToken);

        if(specialityList.Count != specialityRequestIdList.Count)
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Specialities"));

            return false;
        }

        doctorAttendant.Specialities = specialityList;

        return true;
    }
}
