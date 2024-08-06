using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces.Repositories;

public interface ISpecialityRepository
{
    Task<bool> AddAsync(Speciality speciality);
    Task<bool> ExistsAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<List<Speciality>> GetAllAsync();
    Task<Speciality?> GetByIdAsync(int id);
}
