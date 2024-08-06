using Patient.Domain.DataTransferObjects.ContactInfo;
using Patient.Domain.Entities;
using Patient.Domain.Interfaces.Mappers;

namespace Patient.ApplicationServices.Mappers;

public sealed class ContactInfoMapper : IContactInfoMapper
{
    public ContactInfo RequestToDomainCreate(ContactInfoRequest contactInfoRequest) =>
        new()
        {
            Email = contactInfoRequest.Email,
            PhoneNumber = contactInfoRequest.PhoneNumber
        };

    public void RequestToDomainUpdate(ContactInfoRequest contactInfoRequest, ContactInfo contactInfo)
    {
        contactInfo.Email = contactInfoRequest.Email;
        contactInfo.PhoneNumber = contactInfoRequest.PhoneNumber;
    }

    public ContactInfoResponse DomainToResponse(ContactInfo contactInfo) =>
        new()
        {
            Email = contactInfo.Email,
            Id = contactInfo.Id,
            PhoneNumber = contactInfo.PhoneNumber
        };
}
