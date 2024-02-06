using Doctor.Domain.Entities;

namespace Doctor.ApplicationService.Interfaces.Services;
public interface ISpecialityServiceFacade
{
    Task<Speciality?> GetByIdReturnsDomainObjectAsync(int id);
}
