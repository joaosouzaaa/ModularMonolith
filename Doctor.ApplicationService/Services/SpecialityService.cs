using Doctor.ApplicationService.DataTransferObjects.Speciality;
using Doctor.ApplicationService.Interfaces.Mappers;
using Doctor.ApplicationService.Interfaces.Services;
using Doctor.ApplicationService.Services.BaseServices;
using Doctor.Domain.Entities;
using Doctor.Domain.Enums;
using Doctor.Domain.Extensions;
using Doctor.Infrasctructure.Interfaces.Repositories;
using FluentValidation;
using ModularMonolith.Common.Interfaces;

namespace Doctor.ApplicationService.Services;
public sealed class SpecialityService : BaseService<Speciality>, ISpecialityService, ISpecialityServiceFacade
{
    private readonly ISpecialityRepository _specialityRepository;
    private readonly ISpecialityMapper _specialityMapper;

    public SpecialityService(ISpecialityRepository specialityRepository, ISpecialityMapper specialityMapper,
                             INotificationHandler notificationHandler, IValidator<Speciality> validator)
                             : base(notificationHandler, validator)
    {
        _specialityRepository = specialityRepository;
        _specialityMapper = specialityMapper;
    }

    public async Task<bool> AddAsync(SpecialitySave specialitySave)
    {
        var speciality = _specialityMapper.SaveToDomain(specialitySave);

        if (!await ValidateAsync(speciality))
            return false;

        return await _specialityRepository.AddAsync(speciality);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await _specialityRepository.ExistsAsync(id))
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Speciality"));

            return false;
        }

        return await _specialityRepository.DeleteAsync(id);
    }

    public async Task<List<SpecialityResponse>> GetAllAsync()
    {
        var specialityList = await _specialityRepository.GetAllAsync();

        return _specialityMapper.DomainLisToResponseList(specialityList);
    }

    public Task<Speciality?> GetByIdReturnsDomainObjectAsync(int id) =>
        _specialityRepository.GetByIdAsync(id);
}
