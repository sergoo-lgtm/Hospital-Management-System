using HospitalManagementSystemAPIVersion.Service;
using HospitalManagementSystemAPIVersion.DTO.PaymentDTOs;
using HospitalManagementSystemAPIVersion.Model;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystemAPIVersion.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly PaymentService _paymentService;

    public PaymentController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("pay")]
    public async Task<IActionResult> Pay([FromBody] PayDto dto)
    {
        var result = await _paymentService.PayAsync(dto);
        return Ok(new { Paid = result });
    }

    [HttpPut("update-amount")]
    public async Task<IActionResult> UpdateAmount([FromBody] UpdatePaymentDto dto)
    {
        var updatedPayment = await _paymentService.UpdateAmountAsync(dto);
        return Ok(updatedPayment);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var payment = await _paymentService.GetByIdAsync(id);
        return Ok(payment);
    }

    [HttpGet]
    public async Task<IActionResult> GetPage([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var pagedPayments = await _paymentService.GetPageAsync(pageNumber, pageSize);
        return Ok(pagedPayments);
    }
}