using Doctor.Domain.DataTransferObjects.Speciality;
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces.Mappers;

public interface ISpecialityMapper
{
    Speciality SaveToDomain(SpecialitySave specialitySave);
    List<SpecialityResponse> DomainLisToResponseList(List<Speciality> specialityList);
}
