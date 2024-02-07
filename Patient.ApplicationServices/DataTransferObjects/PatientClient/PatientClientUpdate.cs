using Patient.ApplicationServices.DataTransferObjects.ContactInfo;

namespace Patient.ApplicationServices.DataTransferObjects.PatientClient;
public sealed record PatientClientUpdate(int Id,
                                         string Name,
                                         string Address,
                                         ContactInfoRequest ContactInfo);
