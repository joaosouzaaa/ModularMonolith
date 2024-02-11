namespace Patient.Infrastructure.Interfaces.Repositories;
public interface IPatientClientRepositoryFacade
{
    Task<string?> GetEmailByIdAsync(int id);
}
