using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces.Services;

public interface ISpecialityServiceFacade
{
    Task<List<Speciality>> GetAllByIdListAsync(List<int> idList, CancellationToken cancellationToken);
}
