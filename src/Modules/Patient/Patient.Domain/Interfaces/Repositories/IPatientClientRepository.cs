using Patient.Domain.Entities;

namespace Patient.Domain.Interfaces.Repositories;

public interface IPatientClientRepository
{
    Task<bool> AddAsync(PatientClient patientClient, CancellationToken cancellationToken);
    Task<PatientClient?> GetByIdAsync(int id, bool asNoTracking, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(PatientClient patientClient, CancellationToken cancellationToken);
}
