using Doctor.ApplicationService.Interfaces.Mappers;

namespace Doctor.ApplicationService.Mappers;
public sealed class DoctorAttendantMapper(ICertificationMapper certificationMapper) : IDoctorAttendantMapper
{
    private readonly ICertificationMapper _certificationMapper = certificationMapper;
}
