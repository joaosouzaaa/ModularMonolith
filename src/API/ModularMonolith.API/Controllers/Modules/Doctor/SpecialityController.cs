using Doctor.Domain.DataTransferObjects.Speciality;
using Doctor.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Common.Settings.NotificationSettings;

namespace ModularMonolith.API.Controllers.Modules.Doctor;

[Route("api/[controller]")]
[ApiController]
public sealed class SpecialityController(ISpecialityService specialityService) : ControllerBase
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> AddAsync([FromBody] SpecialitySave specialitySave, CancellationToken cancellationToken) =>
        specialityService.AddAsync(specialitySave, cancellationToken);

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> DeleteAsync([FromQuery] int id, CancellationToken cancellationToken) =>
        specialityService.DeleteAsync(id, cancellationToken);

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SpecialityResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<List<SpecialityResponse>> GetAllAsync(CancellationToken cancellationToken) =>
        specialityService.GetAllAsync(cancellationToken);
}
