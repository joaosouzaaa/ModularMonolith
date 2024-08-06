using Moq;
using Patient.ApplicationServices.Mappers;
using Patient.Domain.DataTransferObjects.ContactInfo;
using Patient.Domain.Entities;
using Patient.Domain.Interfaces.Mappers;
using UnitTests.TestBuilders.Patient;

namespace UnitTests.MappersTests.Patient;
public sealed class PatientClientMapperTests
{
    private readonly Mock<IContactInfoMapper> _contactInfoMapperMock;
    private readonly PatientClientMapper _patientClientMapper;

    public PatientClientMapperTests()
    {
        _contactInfoMapperMock = new Mock<IContactInfoMapper>();
        _patientClientMapper = new PatientClientMapper(_contactInfoMapperMock.Object);
    }

    [Fact]
    public void SaveToDomain_SuccessfulScenario()
    {
        // A
        var patientClientSave = PatientClientBuilder.NewObject().SaveBuild();

        var contactInfo = ContactInfoBuilder.NewObject().DomainBuild();
        _contactInfoMapperMock.Setup(c => c.RequestToDomainCreate(It.IsAny<ContactInfoRequest>()))
            .Returns(contactInfo);

        // A
        var patientClientResult = _patientClientMapper.SaveToDomain(patientClientSave);

        // A
        Assert.Equal(patientClientResult.Address, patientClientSave.Address);
        Assert.Equal(patientClientResult.Name, patientClientSave.Name);
        Assert.NotNull(patientClientResult.ContactInfo);
    }

    [Fact]
    public void UpdateToDomain_SuccessfulScenario()
    {
        // A
        var patientClientUpdate = PatientClientBuilder.NewObject().UpdateBuild();
        var patientClientResult = PatientClientBuilder.NewObject().DomainBuild();

        _contactInfoMapperMock.Setup(c => c.RequestToDomainUpdate(It.IsAny<ContactInfoRequest>(), It.IsAny<ContactInfo>()));

        // A
        _patientClientMapper.UpdateToDomain(patientClientUpdate, patientClientResult);

        // A
        Assert.Equal(patientClientResult.Address, patientClientUpdate.Address);
        Assert.Equal(patientClientResult.Name, patientClientUpdate.Name);
        Assert.NotNull(patientClientResult.ContactInfo);
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario()
    {
        // A
        var patientClient = PatientClientBuilder.NewObject().DomainBuild();

        var contactInfoResponse = ContactInfoBuilder.NewObject().ResponseBuild();
        _contactInfoMapperMock.Setup(c => c.DomainToResponse(It.IsAny<ContactInfo>()))
            .Returns(contactInfoResponse);

        // A
        var patientClientResponseResult = _patientClientMapper.DomainToResponse(patientClient);

        // A
        Assert.Equal(patientClientResponseResult.Address, patientClient.Address);
        Assert.Equal(patientClientResponseResult.Id, patientClient.Id);
        Assert.Equal(patientClientResponseResult.Name, patientClient.Name);
        Assert.NotNull(patientClientResponseResult.ContactInfo);
    }
}
