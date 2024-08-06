using Doctor.Domain.DataTransferObjects.Speciality;

namespace Doctor.Domain.Interfaces.Services;

public interface ISpecialityService
{
    Task<bool> AddAsync(SpecialitySave specialitySave);
    Task<bool> DeleteAsync(int id);
    Task<List<SpecialityResponse>> GetAllAsync();
}
