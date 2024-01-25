using Doctor.ApplicationService.DataTransferObjects.DoctorAttendant;
using Doctor.Domain.Entities;

namespace UnitTests.TestBuilders.Doctor;
public sealed class DoctorAttendantBuilder
{
    private readonly int _id = 123;
    private readonly DateOnly _birthDate = new DateOnly(1999, 01, 01);
    private readonly int _experienceYears = 1;
    private readonly string _name = "test";
    private List<Schedule> _scheduleList = [];
    private List<Speciality> _specialityList = [];

    public static DoctorAttendantBuilder NewObject() =>
        new();

    public DoctorAttendant DomainBuild() =>
        new()
        {
            BirthDate = _birthDate,
            Certification = CertificationBuilder.NewObject().DomainBuild(),
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
