using Doctor.Domain.DataTransferObjects.Speciality;
using Doctor.Domain.Entities;

namespace UnitTests.TestBuilders.Doctor;
public sealed class SpecialityBuilder
{
    private readonly int _id = 12;
    private string _name = "test";

    public static SpecialityBuilder NewObject() =>
        new();

    public Speciality DomainBuild() =>
        new()
        {
            Id = _id,
            Name = _name
        };

    public SpecialitySave SaveBuild() =>
        new(_name);

    public SpecialityResponse ResponseBuild() =>
        new()
        {
            Id = _id,
            Name = _name
        };

    public SpecialityBuilder WithName(string name)
    {
        _name = name;

        return this;
    }
}
