using Doctor.ApplicationService.DataTransferObjects.Speciality;
using Doctor.Domain.Entities;

namespace Doctor.ApplicationService.Interfaces.Services;
public interface ISpecialityService
{
    Task<bool> AddAsync(SpecialitySave specialitySave);
    Task<bool> DeleteAsync(int id);
    Task<List<SpecialityResponse>> GetAllAsync();
    Task<Speciality?> GetByIdReturnsDomainObjectAsync(int id);
}
