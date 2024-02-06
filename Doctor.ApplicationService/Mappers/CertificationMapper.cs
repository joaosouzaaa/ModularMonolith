using Doctor.ApplicationService.DataTransferObjects.Certification;
using Doctor.ApplicationService.Interfaces.Mappers;
using Doctor.Domain.Entities;

namespace Doctor.ApplicationService.Mappers;
public sealed class CertificationMapper : ICertificationMapper
{
    public Certification RequestToDomainCreate(CertificationRequest certificationRequest) =>
        new()
        {
            LicenseNumber = certificationRequest.LicenseNumber
        };

    public void RequestToDomainUpdate(CertificationRequest certificationRequest, Certification certification) =>
        certification.LicenseNumber = certificationRequest.LicenseNumber;

    public CertificationResponse DomainToResponse(Certification certification) =>
        new()
        {
            Id = certification.Id,
            LicenseNumber = certification.LicenseNumber
        };
}
