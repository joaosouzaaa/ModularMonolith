using Doctor.ApplicationService.DataTransferObjects.Certification;
using Doctor.ApplicationService.DataTransferObjects.Schedule;
using Doctor.ApplicationService.DataTransferObjects.Speciality;

namespace Doctor.ApplicationService.DataTransferObjects.DoctorAttendant;
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
