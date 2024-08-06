using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces.Services;

public interface ISpecialityServiceFacade
{
    Task<Speciality?> GetByIdReturnsDomainObjectAsync(int id);
}
