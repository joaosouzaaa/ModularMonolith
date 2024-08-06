using Doctor.Domain.DataTransferObjects.Speciality;
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces.Mappers;

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
