using Doctor.ApplicationService.Services.BaseServices;
using Doctor.Domain.DataTransferObjects.DoctorAttendant;
using Doctor.Domain.Entities;
using Doctor.Domain.Enums;
using Doctor.Domain.Extensions;
using Doctor.Domain.Interfaces.Mappers;
using Doctor.Domain.Interfaces.Repositories;
using Doctor.Domain.Interfaces.Services;
using FluentValidation;
using ModularMonolith.Common.Interfaces;
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

    public async Task<bool> AddAsync(DoctorAttendantSave doctorAttendantSave)
    {
        var doctorAttendant = _doctorAttendantMapper.SaveToDomain(doctorAttendantSave);

        if (!await AddSpecialityRelationshipAsync(doctorAttendantSave.SpecialityIds, doctorAttendant.Specialities))
        {
            return false;
        }

        if (!await ValidateAsync(doctorAttendant))
        {
            return false;
        }

        return await _doctorAttendantRepository.AddAsync(doctorAttendant);
    }

    public async Task<bool> UpdateAsync(DoctorAttendantUpdate doctorAttendantUpdate)
    {
        var doctorAttendant = await _doctorAttendantRepository.GetByIdAsync(doctorAttendantUpdate.Id, false);

        if (doctorAttendant is null)
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Doctor Attendant"));

            return false;
        }

        doctorAttendant.Specialities.Clear();
        if (!await AddSpecialityRelationshipAsync(doctorAttendantUpdate.SpecialityIds, doctorAttendant.Specialities))
            return false;

        _doctorAttendantMapper.UpdateToDomain(doctorAttendantUpdate, doctorAttendant);

        if (!await ValidateAsync(doctorAttendant))
            return false;

        return await _doctorAttendantRepository.UpdateAsync(doctorAttendant);
    }

    public async Task<PageList<DoctorAttendantResponse>> GetAllFilteredAndPaginatedAsync(DoctorGetAllFilterRequest filterRequest)
    {
        var getAllFilterArgument = _doctorAttendantMapper.FilterRequestToArgumentDomain(filterRequest);

        var doctorPageList = await _doctorAttendantRepository.GetAllFilteredAndPaginatedAsync(getAllFilterArgument);

        return _doctorAttendantMapper.DomainPageListToResponsePageList(doctorPageList);
    }

    public async Task<DoctorAttendantResponse?> GetByIdAsync(int id)
    {
        var doctorAttendant = await _doctorAttendantRepository.GetByIdAsync(id, true);

        if (doctorAttendant is null)
        {
            return null;
        }

        return _doctorAttendantMapper.DomainToResponse(doctorAttendant);
    }

    private async Task<bool> AddSpecialityRelationshipAsync(List<int> specialityIdList, List<Speciality> specialityList)
    {
        foreach (var specialityId in specialityIdList)
        {
            var speciality = await _specialityServiceFacade.GetByIdReturnsDomainObjectAsync(specialityId);

            if (speciality is null)
            {
                _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Speciality"));

                return false;
            }

            specialityList.Add(speciality);
        }

        return true;
    }
}
