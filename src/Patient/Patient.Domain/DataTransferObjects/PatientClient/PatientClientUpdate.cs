using Patient.Domain.DataTransferObjects.ContactInfo;

namespace Patient.Domain.DataTransferObjects.PatientClient;
public sealed record PatientClientUpdate(int Id,
                                         string Name,
                                         string Address,
                                         ContactInfoRequest ContactInfo);
