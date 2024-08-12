using Doctor.Domain.DataTransferObjects.Speciality;
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces.Mappers;

public interface ISpecialityMapper
{
    List<SpecialityResponse> DomainListToResponseList(List<Speciality> specialityList);
    Speciality SaveToDomain(SpecialitySave specialitySave);
}
