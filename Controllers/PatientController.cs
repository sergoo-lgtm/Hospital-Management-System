using HospitalManagementSystemAPIVersion.Service;
using HospitalManagementSystemAPIVersion.DTO.PatientDTOs;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystemAPIVersion.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly PatientService _patientService;

    public PatientController(PatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreatePatientDto dto)
    {
        var patient = await _patientService.AddAsync(dto);
        return Ok(patient);
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await _patientService.GetByIdAsync(id);
        return Ok(patient);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _patientService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetPage([FromQuery] PatientQueryDto dto)
    {
        var result = await _patientService.GetPageAsync(dto);
        return Ok(result);
    }
}