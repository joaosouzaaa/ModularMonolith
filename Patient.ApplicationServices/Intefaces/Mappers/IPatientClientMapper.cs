using Patient.ApplicationServices.DataTransferObjects.PatientClient;
using Patient.Domain.Entities;

namespace Patient.ApplicationServices.Intefaces.Mappers;
public interface IPatientClientMapper
{
    PatientClient SaveToDomain(PatientClientSave patientClientSave);
    void UpdateToDomain(PatientClientUpdate patientClientUpdate, PatientClient patientClient);
    PatientClientResponse DomainToResponse(PatientClient patientClient);
}
