using Doctor.Domain.DataTransferObjects.Certification;

namespace Doctor.Domain.DataTransferObjects.DoctorAttendant;

public sealed record DoctorAttendantUpdate(
    int Id,
    string Name,
    int ExperienceYears,
    DateTime BirthDate,
    CertificationRequest Certification,
    List<int> SpecialityIds);
