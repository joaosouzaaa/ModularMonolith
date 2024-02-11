﻿using Appointment.ApplicationService.DataTransferObjects.Appointment;
using Appointment.ApplicationService.Interfaces.Mappers;
using Appointment.ApplicationService.Interfaces.Services;
using Appointment.Domain.Entities;
using Appointment.Infrastructure.Interfaces.Publishers;
using Appointment.Infrastructure.Interfaces.Repositories;
using FluentValidation;
using ModularMonolith.Common.Interfaces;

namespace Appointment.ApplicationService.Services;
public sealed class AppointmentTimeService(IAppointmentTimeRepository appointmentTimeRepository, IAppointmentPublisher appointmentPublisher,
                                           IAppointmentTimeMapper appointmentTimeMapper, INotificationHandler notificationHandler,
                                           IValidator<AppointmentTime> validator) : IAppointmentTimeService
{

    public async Task<bool> AddAsync(AppointmentTimeSave appointmentTimeSave)
    {
        var appointmentTime = appointmentTimeMapper.SaveToDomain(appointmentTimeSave);

        if (!await ValidateAsync(appointmentTime))
            return false;

        var addResult = await appointmentTimeRepository.AddAsync(appointmentTime);

        var appointmentTimeCreatedEvent = appointmentTimeMapper.DomainToTimeCreatedEvent(appointmentTime);
        appointmentPublisher.PublishAppointmentTimeCreatedMessage(appointmentTimeCreatedEvent);

        return addResult;
    }

    private async Task<bool> ValidateAsync(AppointmentTime appointmentTime)
    {
        var validationResult = await validator.ValidateAsync(appointmentTime);

        if (validationResult.IsValid)
            return true;

        foreach (var error in validationResult.Errors)
        {
            notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);
        }

        return false;
    }
}
