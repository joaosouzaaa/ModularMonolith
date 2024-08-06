using Patient.Domain.DataTransferObjects.PatientClient;

namespace Patient.Domain.Interfaces.Services;

public interface IPatientClientService
{
    Task<bool> AddAsync(PatientClientSave patientClientSave);
    Task<bool> UpdateAsync(PatientClientUpdate patientClientUpdate);
    Task<PatientClientResponse?> GetByIdAsync(int id);
}
