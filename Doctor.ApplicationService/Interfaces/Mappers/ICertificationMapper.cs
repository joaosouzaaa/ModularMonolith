using Doctor.ApplicationService.DataTransferObjects.Certification;
using Doctor.Domain.Entities;

namespace Doctor.ApplicationService.Interfaces.Mappers;
public interface ICertificationMapper
{
    Certification RequestToDomain(CertificationRequest certificationRequest);
    CertificationResponse DomainToResponse(Certification certification);
}
