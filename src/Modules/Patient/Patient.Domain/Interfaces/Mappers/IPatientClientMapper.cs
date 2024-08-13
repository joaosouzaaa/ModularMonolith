using Patient.Domain.DataTransferObjects.PatientClient;
using Patient.Domain.Entities;

namespace Patient.Domain.Interfaces.Mappers;

public interface IPatientClientMapper
{
    List<PatientClientResponse> DomainListToResponseList(List<PatientClient> patientClientList);
    PatientClientResponse DomainToResponse(PatientClient patientClient);
    PatientClient SaveToDomain(PatientClientSave patientClientSave);
    void UpdateToDomain(PatientClientUpdate patientClientUpdate, PatientClient patientClient);
}
