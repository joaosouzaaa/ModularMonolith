using Doctor.ApplicationService.DataTransferObjects.Certification;

namespace Doctor.ApplicationService.DataTransferObjects.DoctorAttendant;
public sealed record DoctorAttendantUpdate(int Id,
                                           string Name,
                                           int ExperienceYears,
                                           DateOnly BirthDate,
                                           CertificationRequest Certification,
                                           List<int> SpecialityIds);
