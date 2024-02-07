using Patient.ApplicationServices.DataTransferObjects.PatientClient;

namespace Patient.ApplicationServices.Intefaces.Services;
public interface IPatientClientService
{
    Task<bool> AddAsync(PatientClientSave patientClientSave);
    Task<bool> UpdateAsync(PatientClientUpdate patientClientUpdate);
    Task<PatientClientResponse?> GetByIdAsync(int id);
}
