using Patient.Domain.DataTransferObjects.PatientClient;
using Patient.Domain.Entities;

namespace Patient.Domain.Interfaces.Mappers;

public interface IPatientClientMapper
{
    PatientClient SaveToDomain(PatientClientSave patientClientSave);
    void UpdateToDomain(PatientClientUpdate patientClientUpdate, PatientClient patientClient);
    PatientClientResponse DomainToResponse(PatientClient patientClient);
}
