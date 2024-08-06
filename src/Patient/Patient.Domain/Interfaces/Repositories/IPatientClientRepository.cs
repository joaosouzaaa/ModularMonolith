using Patient.Domain.Entities;

namespace Patient.Domain.Interfaces.Repositories;

public interface IPatientClientRepository
{
    Task<bool> AddAsync(PatientClient patientClient);
    Task<bool> UpdateAsync(PatientClient patientClient);
    Task<PatientClient?> GetByIdAsync(int id, bool asNoTracking);
}
