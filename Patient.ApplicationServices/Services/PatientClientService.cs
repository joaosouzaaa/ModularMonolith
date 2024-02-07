using FluentValidation;
using ModularMonolith.Common.Interfaces;
using Patient.ApplicationServices.DataTransferObjects.PatientClient;
using Patient.ApplicationServices.Intefaces.Mappers;
using Patient.ApplicationServices.Intefaces.Services;
using Patient.Domain.Entities;
using Patient.Domain.Enums;
using Patient.Domain.Extensions;
using Patient.Infrastructure.Interfaces.Repositories;

namespace Patient.ApplicationServices.Services;
public sealed class PatientClientService(IPatientClientRepository patientClientRepository, IPatientClientMapper patientClientMapper,
                                         INotificationHandler notificationHandler, IValidator<PatientClient> validator) : IPatientClientService
{
    public async Task<bool> AddAsync(PatientClientSave patientClientSave)
    {
        var patientClient = patientClientMapper.SaveToDomain(patientClientSave);

        if (!await ValidateAsync(patientClient))
            return false;

        return await patientClientRepository.AddAsync(patientClient);
    }

    public async Task<bool> UpdateAsync(PatientClientUpdate patientClientUpdate)
    {
        var patientClient = await patientClientRepository.GetByIdAsync(patientClientUpdate.Id, false);

        if (patientClient is null)
        {
            notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Patient"));

            return false;
        }

        patientClientMapper.UpdateToDomain(patientClientUpdate, patientClient);

        if (!await ValidateAsync(patientClient))
            return false;

        return await patientClientRepository.UpdateAsync(patientClient);
    }

    public async Task<PatientClientResponse?> GetByIdAsync(int id)
    {
        var patientClient = await patientClientRepository.GetByIdAsync(id, true);

        if (patientClient is null)
            return null;

        return patientClientMapper.DomainToResponse(patientClient);
    }

    private async Task<bool> ValidateAsync(PatientClient patientClient)
    {
        var validationResult = await validator.ValidateAsync(patientClient);

        if (validationResult.IsValid)
            return true;

        foreach(var error in validationResult.Errors)
        {
            notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);
        }

        return false;
    }
}
