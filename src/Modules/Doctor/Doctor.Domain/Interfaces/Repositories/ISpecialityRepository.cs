using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces.Repositories;

public interface ISpecialityRepository
{
    Task<bool> AddAsync(Speciality speciality, CancellationToken cancellationToken);
    Task<List<Speciality>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<Speciality>> GetAllAsync(List<int> idList, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
}
