using HospitalManagementSystemAPIVersion.Service;
using HospitalManagementSystemAPIVersion.DTO.DoctorDTOs;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystemAPIVersion.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly DoctorService _doctorService;

    public DoctorController(DoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDoctorDto dto)
    {
        var doctor = await _doctorService.AddAsync(dto);
        return Ok(doctor);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);
        return Ok(doctor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _doctorService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetPage([FromQuery] DoctorQueryDto dto)
    {
        var result = await _doctorService.GetPageAsync(dto);
        return Ok(result);
    }
}