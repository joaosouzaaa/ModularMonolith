﻿using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Common.Settings.NotificationSettings;
using Patient.Domain.DataTransferObjects.PatientClient;
using Patient.Domain.Interfaces.Services;

namespace ModularMonolith.API.Controllers.Modules.Patient;

[Route("api/[controller]")]
[ApiController]
public sealed class PatientClientController(IPatientClientService patientClientService) : ControllerBase
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> AddAsync([FromBody] PatientClientSave patientClientSave, CancellationToken cancellationToken) =>
        patientClientService.AddAsync(patientClientSave, cancellationToken);

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> UpdateAsync([FromBody] PatientClientUpdate patientClientUpdate, CancellationToken cancellationToken) =>
        patientClientService.UpdateAsync(patientClientUpdate, cancellationToken);

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PatientClientResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<List<PatientClientResponse>> GetAllAsync(CancellationToken cancellationToken) =>
        patientClientService.GetAllAsync(cancellationToken);

    [HttpGet("get-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientClientResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<PatientClientResponse?> GetByIdAsync([FromQuery] int id, CancellationToken cancellationToken) =>
        patientClientService.GetByIdAsync(id, cancellationToken);
}
