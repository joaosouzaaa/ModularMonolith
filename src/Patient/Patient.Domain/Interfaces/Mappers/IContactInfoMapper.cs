using Patient.Domain.DataTransferObjects.ContactInfo;
using Patient.Domain.Entities;

namespace Patient.Domain.Interfaces.Mappers;

public interface IContactInfoMapper
{
    ContactInfoResponse DomainToResponse(ContactInfo contactInfo);
    ContactInfo RequestToDomainCreate(ContactInfoRequest contactInfoRequest);
    void RequestToDomainUpdate(ContactInfoRequest contactInfoRequest, ContactInfo contactInfo);
}
