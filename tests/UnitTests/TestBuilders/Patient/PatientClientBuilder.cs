using Patient.Domain.DataTransferObjects.ContactInfo;
using Patient.Domain.DataTransferObjects.PatientClient;
using Patient.Domain.Entities;

namespace UnitTests.TestBuilders.Patient;
public sealed class PatientClientBuilder
{
    private readonly int _id = 123;
    private string _name = "test";
    private string _address = "test";
    private ContactInfo _contactInfo = ContactInfoBuilder.NewObject().DomainBuild();
    private readonly ContactInfoRequest _contactInfoRequest = ContactInfoBuilder.NewObject().RequestBuild();

    public static PatientClientBuilder NewObject() =>
        new();

    public PatientClient DomainBuild() =>
        new()
        {
            Id = _id,
            Name = _name,
            Address = _address,
            ContactInfo = _contactInfo
        };

    public PatientClientSave SaveBuild() =>
        new(_name,
            _address,
            _contactInfoRequest);

    public PatientClientUpdate UpdateBuild() =>
        new(_id,
            _name,
            _address,
            _contactInfoRequest);

    public PatientClientResponse ResponseBuild() =>
        new()
        {
            Address = _address,
            ContactInfo = ContactInfoBuilder.NewObject().ResponseBuild(),
            Id = _id,
            Name = _name
        };

    public PatientClientBuilder WithName(string name)
    {
        _name = name;

        return this;
    }

    public PatientClientBuilder WithAddress(string address)
    {
        _address = address;

        return this;
    }

    public PatientClientBuilder WithContactInfo(ContactInfo contactInfo)
    {
        _contactInfo = contactInfo;

        return this;
    }
}
