using Patient.Domain.DataTransferObjects.ContactInfo;

namespace Patient.Domain.DataTransferObjects.PatientClient;

public sealed record PatientClientSave(
    string Name,
    string Address,
    ContactInfoRequest ContactInfo);
