using Appointment.Domain.Contracts;
using Appointment.Domain.DataTransferObjects.Appointment;
using Appointment.Domain.Entities;

namespace UnitTests.TestBuilders.Appointment;

public sealed class AppointmentTimeBuilder
{
    private int _doctorAttendantId = 123;
    private int _patientClientId = 98;
    private DateTime _time = DateTime.Now.AddDays(2);

    public static AppointmentTimeBuilder NewObject() =>
        new();

    public AppointmentTime DomainBuild() =>
        new()
        {
            DoctorAttendantId = _doctorAttendantId,
            Id = 123,
            PatientClientId = _patientClientId,
            Time = _time
        };

    public AppointmentTimeSave SaveBuild() =>
        new(_time,
            _doctorAttendantId,
            _patientClientId);

    public AppointmentTimeCreatedEvent CreatedEventBuild() =>
        new(_time,
            _doctorAttendantId,
            _patientClientId);

    public AppointmentTimeBuilder WithDoctorAttendantId(int doctorAttendantId)
    {
        _doctorAttendantId = doctorAttendantId;

        return this;
    }

    public AppointmentTimeBuilder WithPatientClientId(int patientClientId)
    {
        _patientClientId = patientClientId;

        return this;
    }

    public AppointmentTimeBuilder WithTime(DateTime time)
    {
        _time = time;

        return this;
    }
}
