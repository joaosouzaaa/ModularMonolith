using Patient.Domain.DataTransferObjects.PatientClient;

namespace Patient.Domain.Interfaces.Services;

public interface IPatientClientService
{
    Task<bool> AddAsync(PatientClientSave patientClientSave, CancellationToken cancellationToken);
    Task<PatientClientResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(PatientClientUpdate patientClientUpdate, CancellationToken cancellationToken);
}
