using Patient.Domain.DataTransferObjects.ContactInfo;

namespace Patient.Domain.DataTransferObjects.PatientClient;

public sealed record PatientClientResponse(
    int Id,
    string Name,
    string Address,
    ContactInfoResponse ContactInfo);
