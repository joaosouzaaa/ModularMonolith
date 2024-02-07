using Patient.ApplicationServices.DataTransferObjects.ContactInfo;
using Patient.Domain.Entities;

namespace Patient.ApplicationServices.Intefaces.Mappers;
public interface IContactInfoMapper
{
    ContactInfo RequestToDomainCreate(ContactInfoRequest contactInfoRequest);
    void RequestToDomainUpdate(ContactInfoRequest contactInfoRequest, ContactInfo contactInfo);
    ContactInfoResponse DomainToResponse(ContactInfo contactInfo);
}
