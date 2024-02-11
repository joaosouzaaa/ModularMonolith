using Appointment.ApplicationService.DataTransferObjects.Appointment;
using Appointment.ApplicationService.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Common.Settings.NotificationSettings;

namespace ModularMonolith.API.Controllers.Modules.Appointment;
[Route("api/[controller]")]
[ApiController]
public sealed class AppointmentTimeController(IAppointmentTimeService appointmentTimeService) : ControllerBase
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> AddAsync([FromBody] AppointmentTimeSave appointmentTimeSave) =>
        appointmentTimeService.AddAsync(appointmentTimeSave);
}
