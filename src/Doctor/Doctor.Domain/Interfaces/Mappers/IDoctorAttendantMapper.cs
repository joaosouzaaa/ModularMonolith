using Doctor.Domain.Arguments;
using Doctor.Domain.DataTransferObjects.DoctorAttendant;
using Doctor.Domain.Entities;
using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.Domain.Interfaces.Mappers;

public interface IDoctorAttendantMapper
{
    DoctorAttendant SaveToDomain(DoctorAttendantSave doctorAttendantSave);
    void UpdateToDomain(DoctorAttendantUpdate doctorAttendantUpdate, DoctorAttendant doctorAttendant);
    DoctorAttendantResponse DomainToResponse(DoctorAttendant doctorAttendant);
    DoctorGetAllFilterArgument FilterRequestToArgumentDomain(DoctorGetAllFilterRequest doctorGetAllFilterRequest);
    PageList<DoctorAttendantResponse> DomainPageListToResponsePageList(PageList<DoctorAttendant> doctorAttendantPageList);
}
