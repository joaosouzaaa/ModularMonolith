using Doctor.ApplicationService.Services.BaseServices;
using Doctor.Domain.DataTransferObjects.Speciality;
using Doctor.Domain.Entities;
using Doctor.Domain.Enums;
using Doctor.Domain.Interfaces.Mappers;
using Doctor.Domain.Interfaces.Repositories;
using Doctor.Domain.Interfaces.Services;
using FluentValidation;
using ModularMonolith.Common.Extensions;
using ModularMonolith.Common.Interfaces.Settings;

namespace Doctor.ApplicationService.Services;

public sealed class SpecialityService
    : BaseService<Speciality>,
    ISpecialityService,
    ISpecialityServiceFacade
{
    private readonly ISpecialityRepository _specialityRepository;
    private readonly ISpecialityMapper _specialityMapper;

    public SpecialityService(
        ISpecialityRepository specialityRepository,
        ISpecialityMapper specialityMapper,
        INotificationHandler notificationHandler,
        IValidator<Speciality> validator)
        : base(
            notificationHandler,
            validator)
    {
        _specialityRepository = specialityRepository;
        _specialityMapper = specialityMapper;
    }

    public async Task<bool> AddAsync(SpecialitySave specialitySave, CancellationToken cancellationToken)
    {
        var speciality = _specialityMapper.SaveToDomain(specialitySave);

        if (!await IsValidAsync(speciality, cancellationToken))
        {
            return false;
        }

        return await _specialityRepository.AddAsync(speciality, cancellationToken);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (!await _specialityRepository.ExistsAsync(id, cancellationToken))
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Speciality"));

            return false;
        }

        return await _specialityRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<List<SpecialityResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var specialityList = await _specialityRepository.GetAllAsync(cancellationToken);

        return _specialityMapper.DomainListToResponseList(specialityList);
    }

    public Task<List<Speciality>> GetAllByIdListAsync(List<int> idList, CancellationToken cancellationToken) =>
        _specialityRepository.GetAllAsync(idList, cancellationToken);
}
