using Doctor.ApplicationService.DataTransferObjects.Speciality;
using Doctor.ApplicationService.Interfaces.Mappers;
using Doctor.ApplicationService.Interfaces.Services;
using Doctor.Infrasctructure.Interfaces.Repositories;

namespace Doctor.ApplicationService.Services;
public sealed class SpecialityService(ISpecialityRepository specialityRepository, ISpecialityMapper specialityMapper) : ISpecialityService
{
    private readonly ISpecialityRepository _specialityRepository = specialityRepository;
    private readonly ISpecialityMapper _specialityMapper = specialityMapper;

    public async Task<bool> AddAsync(SpecialitySave specialitySave)
    {
        var speciality = _specialityMapper.SaveToDomain(specialitySave);

        return true;
    }
}
