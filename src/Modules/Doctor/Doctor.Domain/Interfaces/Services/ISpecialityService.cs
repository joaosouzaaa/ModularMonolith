using Doctor.Domain.DataTransferObjects.Speciality;

namespace Doctor.Domain.Interfaces.Services;

public interface ISpecialityService
{
    Task<bool> AddAsync(SpecialitySave specialitySave, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<List<SpecialityResponse>> GetAllAsync(CancellationToken cancellationToken);
}
