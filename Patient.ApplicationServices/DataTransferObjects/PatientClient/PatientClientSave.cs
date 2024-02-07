using Patient.ApplicationServices.DataTransferObjects.ContactInfo;

namespace Patient.ApplicationServices.DataTransferObjects.PatientClient;
public sealed record PatientClientSave(string Name, 
                                       string Address,
                                       ContactInfoRequest ContactInfo);
