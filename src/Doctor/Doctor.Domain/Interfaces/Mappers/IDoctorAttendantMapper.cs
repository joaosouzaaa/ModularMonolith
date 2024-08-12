using Doctor.Domain.Arguments;
using Doctor.Domain.DataTransferObjects.DoctorAttendant;
using Doctor.Domain.Entities;
using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.Domain.Interfaces.Mappers;

public interface IDoctorAttendantMapper
{
    PageList<DoctorAttendantResponse> DomainPageListToResponsePageList(PageList<DoctorAttendant> doctorAttendantPageList);
    DoctorAttendantResponse DomainToResponse(DoctorAttendant doctorAttendant);
    DoctorGetAllFilterArgument FilterRequestToArgumentDomain(DoctorGetAllFilterRequest doctorGetAllFilterRequest);
    DoctorAttendant SaveToDomain(DoctorAttendantSave doctorAttendantSave);
    void UpdateToDomain(DoctorAttendantUpdate doctorAttendantUpdate, DoctorAttendant doctorAttendant);
}
