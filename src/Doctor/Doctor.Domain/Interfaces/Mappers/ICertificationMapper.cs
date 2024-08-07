using Doctor.Domain.DataTransferObjects.Certification;
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces.Mappers;

public interface ICertificationMapper
{
    CertificationResponse DomainToResponse(Certification certification);
    Certification RequestToDomainCreate(CertificationRequest certificationRequest);
    void RequestToDomainUpdate(CertificationRequest certificationRequest, Certification certification);
}
