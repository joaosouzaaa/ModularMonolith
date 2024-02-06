using Doctor.ApplicationService.DataTransferObjects.Speciality;
using Doctor.Domain.Entities;

namespace Doctor.ApplicationService.Interfaces.Mappers;
public interface ISpecialityMapper
{
    Speciality SaveToDomain(SpecialitySave specialitySave);
    List<SpecialityResponse> DomainLisToResponseList(List<Speciality> specialityList);
}
