using HospitalManagementSystemAPIVersion.Service;
using HospitalManagementSystemAPIVersion.DTO.PrescriptionDTOs;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystemAPIVersion.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly PrescriptionService _prescriptionService;

    public PrescriptionController(PrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePrescriptionDto dto)
    {
        var prescription = await _prescriptionService.AddAsync(dto);
        return Ok(prescription);
    }

    [HttpPut("update-notes")]
    public async Task<IActionResult> UpdateNotes([FromBody] UpdateNotesDto dto)
    {
        var updated = await _prescriptionService.UpdateNotesAsync(dto);
        return Ok(updated);
    }

    [HttpPut("update-medications")]
    public async Task<IActionResult> UpdateMedications([FromBody] UpdateMedicationsDto dto)
    {
        var updated = await _prescriptionService.UpdateMedicationsAsync(dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _prescriptionService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var prescription = await _prescriptionService.GetByIdAsync(id);
        return Ok(prescription);
    }

    [HttpGet]
    public async Task<IActionResult> GetPage([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
    {
        var result = await _prescriptionService.GetPageAsync(pageNumber, pageSize, search);
        return Ok(result);
    }
}