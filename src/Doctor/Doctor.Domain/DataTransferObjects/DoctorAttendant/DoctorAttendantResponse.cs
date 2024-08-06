using Doctor.Domain.DataTransferObjects.Certification;
using Doctor.Domain.DataTransferObjects.Schedule;
using Doctor.Domain.DataTransferObjects.Speciality;

namespace Doctor.Domain.DataTransferObjects.DoctorAttendant;

public sealed class DoctorAttendantResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int ExperienceYears { get; set; }
    public required DateOnly BirthDate { get; set; }

    public required CertificationResponse Certification { get; set; }
    public required List<ScheduleResponse> Schedules { get; set; }
    public required List<SpecialityResponse> Specialities { get; set; }
}
