using Patient.Domain.Entities;

namespace UnitTests.TestBuilders.Patient;
public sealed class PatientClientBuilder
{
    private readonly int _id = 123;
    private string _name = "test";
    private string _address = "test";
    private ContactInfo _contactInfo = ContactInfoBuilder.NewObject().DomainBuild();

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
