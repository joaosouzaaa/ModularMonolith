using Doctor.Domain.DataTransferObjects.Speciality;
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces.Mappers;

namespace Doctor.ApplicationService.Mappers;

public sealed class SpecialityMapper : ISpecialityMapper
{
    public List<SpecialityResponse> DomainListToResponseList(List<Speciality> specialityList) =>
        specialityList.Select(DomainToResponse).ToList();

    public Speciality SaveToDomain(SpecialitySave specialitySave) =>
        new()
        {
            Name = specialitySave.Name
        };

    private static SpecialityResponse DomainToResponse(Speciality speciality) =>
        new(speciality.Id,
            speciality.Name);
}
