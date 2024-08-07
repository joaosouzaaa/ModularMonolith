using Doctor.Domain.DataTransferObjects.Certification;
using Doctor.Domain.DataTransferObjects.Schedule;
using Doctor.Domain.DataTransferObjects.Speciality;

namespace Doctor.Domain.DataTransferObjects.DoctorAttendant;

public sealed record DoctorAttendantResponse(
    int Id, 
    string Name, 
    int ExperienceYears, 
    DateOnly BirthDate, 
    CertificationResponse Certification, 
    List<ScheduleResponse> Schedules, 
    List<SpecialityResponse> Specialities);
