namespace Patient.Domain.Interfaces.Repositories;

public interface IPatientClientRepositoryFacade
{
    Task<string?> GetEmailByIdAsync(int id, CancellationToken cancellationToken);
}
