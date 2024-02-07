using Patient.ApplicationServices.DataTransferObjects.ContactInfo;
using Patient.Domain.Entities;

namespace UnitTests.TestBuilders.Patient;
public sealed class ContactInfoBuilder
{
    private string _email = "valid@email.com";
    private readonly int _id = 1;
    private string _phoneNumber = "12345678911";

    public static ContactInfoBuilder NewObject() =>
        new();

    public ContactInfo DomainBuild() =>
        new()
        {
            Email = _email,
            Id = _id,
            PatientClientId = 1,
            PhoneNumber = _phoneNumber
        };

    public ContactInfoRequest RequestBuild() =>
        new(_phoneNumber, 
            _email);

    public ContactInfoResponse ResponseBuild() =>
        new()
        {
            Email = _email,
            Id = _id,
            PhoneNumber = _phoneNumber
        };

    public ContactInfoBuilder WithEmail(string email)
    {
        _email = email;

        return this;
    }

    public ContactInfoBuilder WithPhoneNumber(string phoneNumber)
    {
        _phoneNumber = phoneNumber;

        return this;
    }
}
