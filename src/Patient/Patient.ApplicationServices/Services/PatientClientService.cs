using FluentValidation;
using ModularMonolith.Common.Extensions;
using ModularMonolith.Common.Interfaces.Settings;
using Patient.Domain.DataTransferObjects.PatientClient;
using Patient.Domain.Entities;
using Patient.Domain.Enums;
using Patient.Domain.Interfaces.Mappers;
using Patient.Domain.Interfaces.Repositories;
using Patient.Domain.Interfaces.Services;

namespace Patient.ApplicationServices.Services;

public sealed class PatientClientService(
    IPatientClientRepository patientClientRepository,
    IPatientClientMapper patientClientMapper,
    INotificationHandler notificationHandler,
    IValidator<PatientClient> validator)
    : IPatientClientService
{
    public async Task<bool> AddAsync(PatientClientSave patientClientSave, CancellationToken cancellationToken)
    {
        var patientClient = patientClientMapper.SaveToDomain(patientClientSave);

        if (!await IsValidAsync(patientClient, cancellationToken))
        {
            return false;
        }

        return await patientClientRepository.AddAsync(patientClient, cancellationToken);
    }

    public async Task<PatientClientResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var patientClient = await patientClientRepository.GetByIdAsync(id, true, cancellationToken);

        if (patientClient is null)
        {
            return null;
        }

        return patientClientMapper.DomainToResponse(patientClient);
    }

    public async Task<bool> UpdateAsync(PatientClientUpdate patientClientUpdate, CancellationToken cancellationToken)
    {
        var patientClient = await patientClientRepository.GetByIdAsync(patientClientUpdate.Id, false, cancellationToken);

        if (patientClient is null)
        {
            notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Patient"));

            return false;
        }

        patientClientMapper.UpdateToDomain(patientClientUpdate, patientClient);

        if (!await IsValidAsync(patientClient, cancellationToken))
        {
            return false;
        }

        return await patientClientRepository.UpdateAsync(patientClient, cancellationToken);
    }

    private async Task<bool> IsValidAsync(PatientClient patientClient, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(patientClient, cancellationToken);

        if (validationResult.IsValid)
        {
            return true;
        }

        foreach (var error in validationResult.Errors)
        {
            notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);
        }

        return false;
    }
}
