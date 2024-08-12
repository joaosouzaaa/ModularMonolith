using Doctor.Domain.DataTransferObjects.Certification;
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces.Mappers;

namespace Doctor.ApplicationService.Mappers;

public sealed class CertificationMapper : ICertificationMapper
{
    public CertificationResponse DomainToResponse(Certification certification) =>
        new(certification.Id,
            certification.LicenseNumber);

    public Certification RequestToDomainCreate(CertificationRequest certificationRequest) =>
        new()
        {
            LicenseNumber = certificationRequest.LicenseNumber
        };

    public void RequestToDomainUpdate(CertificationRequest certificationRequest, Certification certification) =>
        certification.LicenseNumber = certificationRequest.LicenseNumber;
}
