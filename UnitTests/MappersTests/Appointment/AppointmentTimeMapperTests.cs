﻿using Appointment.ApplicationService.Mappers;
using UnitTests.TestBuilders.Appointment;

namespace UnitTests.MappersTests.Appointment;
public sealed class AppointmentTimeMapperTests
{
    private readonly AppointmentTimeMapper _appointmentTimeMapper;

    public AppointmentTimeMapperTests()
    {
        _appointmentTimeMapper = new AppointmentTimeMapper();
    }

    [Fact]
    public void SaveToDomain_SuccessfulScenario()
    {
        // A
        var appointmentTimeSave = AppointmentTimeBuilder.NewObject().SaveBuild();

        // A
        var appointmentTimeResult = _appointmentTimeMapper.SaveToDomain(appointmentTimeSave);

        // A
        Assert.Equal(appointmentTimeResult.DoctorAttendantId, appointmentTimeSave.DoctorAttendantId);
        Assert.Equal(appointmentTimeResult.PatientClientId, appointmentTimeSave.PatientClientId);
        Assert.Equal(appointmentTimeResult.Time, appointmentTimeSave.Time);
    }

    [Fact]
    public void DomainToTimeCreatedEvent()
    {
        // A
        var appointmentTime = AppointmentTimeBuilder.NewObject().DomainBuild();

        // A
        var appointmentTimeCreatedEventResult = _appointmentTimeMapper.DomainToTimeCreatedEvent(appointmentTime);

        // A
        Assert.Equal(appointmentTimeCreatedEventResult.Time, appointmentTime.Time);
        Assert.Equal(appointmentTimeCreatedEventResult.DoctorAttendantId, appointmentTime.DoctorAttendantId);
        Assert.Equal(appointmentTimeCreatedEventResult.PatientClientId, appointmentTime.PatientClientId);
    }
}
