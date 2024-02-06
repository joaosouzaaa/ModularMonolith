using Doctor.ApplicationService.DataTransferObjects.Speciality;

namespace Doctor.ApplicationService.Interfaces.Services;
public interface ISpecialityService
{
    Task<bool> AddAsync(SpecialitySave specialitySave);
    Task<bool> DeleteAsync(int id);
    Task<List<SpecialityResponse>> GetAllAsync();
}
