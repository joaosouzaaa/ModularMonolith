using Doctor.ApplicationService.DataTransferObjects.DoctorAttendant;
using Doctor.Domain.Entities;

namespace UnitTests.TestBuilders.Doctor;
public sealed class DoctorAttendantBuilder
{
    private readonly int _id = 123;
    private DateOnly _birthDate = new(DateTime.Now.AddYears(-20).Year, 01, 01);
    private Certification _certification = CertificationBuilder.NewObject().DomainBuild();
    private int _experienceYears = 1;
    private string _name = "test";
    private List<Schedule> _scheduleList = [];
    private List<Speciality> _specialityList = [];

    public static DoctorAttendantBuilder NewObject() =>
        new();

    public DoctorAttendant DomainBuild() =>
        new()
        {
            BirthDate = _birthDate,
            Certification = _certification,
            CertificationId = 123,
            ExperienceYears = _experienceYears,
            Id = _id,
            Name = _name,
            Schedules = _scheduleList,
            Specialities = _specialityList
        };

    public DoctorAttendantSave SaveBuild() =>
        new(_name,
            _experienceYears,
            _birthDate,
            CertificationBuilder.NewObject().RequestBuild(),
            []);

    public DoctorAttendantUpdate UpdateBuild() =>
        new(_id,
            _name,
            _experienceYears,
            _birthDate,
            CertificationBuilder.NewObject().RequestBuild(),
            []);

    public DoctorAttendantResponse ResponseBuild() =>
        new()
        {
            BirthDate = _birthDate,
            Certification = CertificationBuilder.NewObject().ResponseBuild(),
            ExperienceYears = _experienceYears,
            Id = _id,
            Name = _name,
            Schedules = [],
            Specialities = []
        };

    public DoctorAttendantBuilder WithBirthDate(DateOnly birthDate)
    {
        _birthDate = birthDate;

        return this;
    }

    public DoctorAttendantBuilder WithCertification(Certification certification)
    {
        _certification = certification;

        return this;
    }

    public DoctorAttendantBuilder WithExperienceYears(int experienceYears)
    {
        _experienceYears = experienceYears;

        return this;
    }

    public DoctorAttendantBuilder WithName(string name)
    {
        _name = name;

        return this;
    }

    public DoctorAttendantBuilder WithScheduleList(List<Schedule> scheduleList)
    {
        _scheduleList = scheduleList;

        return this;
    }

    public DoctorAttendantBuilder WithSpecialityList(List<Speciality> specialityList)
    {
        _specialityList = specialityList;

        return this;
    }
}
