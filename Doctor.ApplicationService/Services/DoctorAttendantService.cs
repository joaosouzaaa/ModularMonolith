using Doctor.ApplicationService.DataTransferObjects.DoctorAttendant;
using Doctor.ApplicationService.Interfaces.Mappers;
using Doctor.ApplicationService.Interfaces.Services;
using Doctor.ApplicationService.Services.BaseServices;
using Doctor.Domain.Entities;
using Doctor.Infrasctructure.Interfaces.Repositories;
using FluentValidation;
using ModularMonolith.Common.Interfaces;

namespace Doctor.ApplicationService.Services;
public sealed class DoctorAttendantService(IDoctorAttendantRepository doctorAttendantRepository, IDoctorAttendantMapper doctorAttendantMapper,
                                           INotificationHandler notificationHandler, IValidator<DoctorAttendant> validator) 
                                           : BaseService<DoctorAttendant>(notificationHandler, validator), IDoctorAttendantService
{
    private readonly IDoctorAttendantRepository _doctorAttendantRepository = doctorAttendantRepository;
    private readonly IDoctorAttendantMapper _doctorAttendantMapper = doctorAttendantMapper;

    public async Task<bool> AddAsync(DoctorAttendantSave doctorAttendantSave)
    {
        var doctorAttendant = _doctorAttendantMapper.SaveToDomain(doctorAttendantSave);

        if (!await ValidateAsync(doctorAttendant))
            return false;

        return await _doctorAttendantRepository.AddAsync(doctorAttendant);
    }
}
