using Doctor.ApplicationService.DataTransferObjects.Speciality;
using Doctor.ApplicationService.Interfaces.Mappers;
using Doctor.Domain.Entities;

namespace Doctor.ApplicationService.Mappers;
public sealed class SpecialityMapper : ISpecialityMapper
{
    public Speciality SaveToDomain(SpecialitySave specialitySave) =>
        new()
        {
            Name = specialitySave.Name
        };

    public List<SpecialityResponse> DomainLisToResponseList(List<Speciality> specialityList) =>
        specialityList.Select(DomainToResponse).ToList();

    private SpecialityResponse DomainToResponse(Speciality speciality) =>
        new()
        {
            Id = speciality.Id,
            Name = speciality.Name
        };
}
