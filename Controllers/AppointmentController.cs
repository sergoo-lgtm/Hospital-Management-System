using HospitalManagementSystemAPIVersion.Service;
using HospitalManagementSystemAPIVersion.DTO.AppointmentDTOs;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystemAPIVersion.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentService _appointmentService;

    public AppointmentController(AppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateAppointmentDto dto)
    {
        var appointment = await _appointmentService.AddAsync(dto);
        return Ok(appointment);
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(int id, [FromBody] CompleteAppointmentDto dto)
    {
        dto.AppointmentId = id; 
        var completed = await _appointmentService.CompleteAsync(dto);
        return Ok(completed);
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var appointment = await _appointmentService.GetByIdAsync(id);
        return Ok(appointment);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _appointmentService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetPage([FromQuery] AppointmentQueryDto dto)
    {
        var result = await _appointmentService.GetPageAsync(dto);
        return Ok(result);
    }
}