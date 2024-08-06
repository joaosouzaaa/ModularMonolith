using Doctor.Domain.DataTransferObjects.Certification;

namespace Doctor.Domain.DataTransferObjects.DoctorAttendant;

public sealed record DoctorAttendantSave(
    string Name,
    int ExperienceYears,
    DateTime BirthDate,
    CertificationRequest Certification,
    List<int> SpecialityIds);
