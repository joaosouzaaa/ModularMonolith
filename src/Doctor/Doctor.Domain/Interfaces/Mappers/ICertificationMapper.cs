using Doctor.Domain.DataTransferObjects.Certification;
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces.Mappers;

public interface ICertificationMapper
{
    Certification RequestToDomainCreate(CertificationRequest certificationRequest);
    void RequestToDomainUpdate(CertificationRequest certificationRequest, Certification certification);
    CertificationResponse DomainToResponse(Certification certification);
}
