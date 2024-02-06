﻿using Doctor.ApplicationService.DataTransferObjects.DoctorAttendant;
using Doctor.ApplicationService.Interfaces.Mappers;
using Doctor.ApplicationService.Interfaces.Services;
using Doctor.ApplicationService.Services.BaseServices;
using Doctor.Domain.Entities;
using Doctor.Domain.Enums;
using Doctor.Domain.Extensions;
using Doctor.Infrasctructure.Interfaces.Repositories;
using FluentValidation;
using ModularMonolith.Common.Interfaces;
using ModularMonolith.Common.Settings.PaginationSettings;

namespace Doctor.ApplicationService.Services;
public sealed class DoctorAttendantService : BaseService<DoctorAttendant>, IDoctorAttendantService
{
    private readonly IDoctorAttendantRepository _doctorAttendantRepository;
    private readonly IDoctorAttendantMapper _doctorAttendantMapper;

    public DoctorAttendantService(IDoctorAttendantRepository doctorAttendantRepository, IDoctorAttendantMapper doctorAttendantMapper,
                                  INotificationHandler notificationHandler, IValidator<DoctorAttendant> validator) 
                                  : base(notificationHandler, validator)
    {
        _doctorAttendantRepository = doctorAttendantRepository;
        _doctorAttendantMapper = doctorAttendantMapper;
    }

    public async Task<bool> AddAsync(DoctorAttendantSave doctorAttendantSave)
    {
        var doctorAttendant = _doctorAttendantMapper.SaveToDomain(doctorAttendantSave);

        if (!await ValidateAsync(doctorAttendant))
            return false;

        return await _doctorAttendantRepository.AddAsync(doctorAttendant);
    }

    public async Task<bool> UpdateAsync(DoctorAttendantUpdate doctorAttendantUpdate)
    {
        var doctorAttendant = await _doctorAttendantRepository.GetByIdAsync(doctorAttendantUpdate.Id, false);

        if (doctorAttendant is null)
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Doctor Attendant"));

            return false;
        }

        _doctorAttendantMapper.UpdateToDomain(doctorAttendantUpdate, doctorAttendant);

        if (!await ValidateAsync(doctorAttendant))
            return false;

        return await _doctorAttendantRepository.UpdateAsync(doctorAttendant);
    }

    public async Task<PageList<DoctorAttendantResponse>> GetAllFilteredAndPaginatedAsync(DoctorGetAllFilterRequest filterRequest)
    {
        var getAllFilterArgument = _doctorAttendantMapper.FilterRequestToArgumentDomain(filterRequest);

        var doctorPageList = await _doctorAttendantRepository.GetAllFilteredAndPaginatedAsync(getAllFilterArgument);

        return _doctorAttendantMapper.DomainPageListToResponsePageList(doctorPageList);
    }

    public async Task<DoctorAttendantResponse?> GetByIdAsync(int id)
    {
        var doctorAttendant = await _doctorAttendantRepository.GetByIdAsync(id, true);

        if (doctorAttendant is null)
            return null;

        return _doctorAttendantMapper.DomainToResponse(doctorAttendant);
    }
}