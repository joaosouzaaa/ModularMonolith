namespace Appointment.Domain.Constants;

public static class RabbitMQConstants
{
    public const string AppointmentExchange = "appointment_exchange";
    public const string ApointmentDoctorQueue = "appointment_doctor_queue";
    public const string ApointmentPatientQueue = "appointment_patient_queue";
}
