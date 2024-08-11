using Doctor.Domain.DataTransferObjects.DoctorAttendant;
using Doctor.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Common.Settings.NotificationSettings;
using ModularMonolith.Common.Settings.PaginationSettings;

namespace ModularMonolith.API.Controllers.Modules.Doctor;

[Route("api/[controller]")]
[ApiController]
public sealed class DoctorAttendantController(IDoctorAttendantService doctorAttendantService) : ControllerBase
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> AddAsync([FromBody] DoctorAttendantSave doctorAttendantSave, CancellationToken cancellationToken) =>
        doctorAttendantService.AddAsync(doctorAttendantSave, cancellationToken);

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> UpdateAsync([FromBody] DoctorAttendantUpdate doctorAttendantUpdate, CancellationToken cancellationToken) =>
        doctorAttendantService.UpdateAsync(doctorAttendantUpdate, cancellationToken);

    [HttpGet("get-all-filtered")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageList<DoctorAttendantResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<PageList<DoctorAttendantResponse>> GetAllFilteredAndPaginatedAsync(
        [FromQuery] DoctorGetAllFilterRequest filterRequest,
        CancellationToken cancellationToken) =>
        doctorAttendantService.GetAllFilteredAndPaginatedAsync(filterRequest, cancellationToken);

    [HttpGet("get-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DoctorAttendantResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<DoctorAttendantResponse?> GetByIdAsync([FromQuery] int id, CancellationToken cancellationToken) =>
        doctorAttendantService.GetByIdAsync(id, cancellationToken);
}
