using Patient.Domain.DataTransferObjects.ContactInfo;
using Patient.Domain.Entities;

namespace Patient.Domain.Interfaces.Mappers;

public interface IContactInfoMapper
{
    ContactInfo RequestToDomainCreate(ContactInfoRequest contactInfoRequest);
    void RequestToDomainUpdate(ContactInfoRequest contactInfoRequest, ContactInfo contactInfo);
    ContactInfoResponse DomainToResponse(ContactInfo contactInfo);
}
