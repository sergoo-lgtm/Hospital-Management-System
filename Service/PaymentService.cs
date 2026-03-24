using HospitalManagementSystemAPIVersion.DTO.PaymentDTOs;
using HospitalManagementSystemAPIVersion.Model;
using HospitalManagementSystemAPIVersion.DTO;
using HospitalManagementSystemAPIVersion.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPIVersion.Service;

public class PaymentService
{
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> PayAsync(PayDto dto)
    {
        var payment = await _unitOfWork.Payments.GetAll
            .FirstOrDefaultAsync(p => p.AppointmentId == dto.AppointmentId);

        if (payment == null) throw new KeyNotFoundException("Payment not found");

        var result = payment.Pay(dto.AppointmentId);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<Payment> UpdateAmountAsync(UpdatePaymentDto dto)
    {
        var payment = await _unitOfWork.Payments.GetByIdAsync(dto.PaymentId);

        if (payment == null) throw new KeyNotFoundException("Payment not found");

        payment.UpdateAmount(dto.NewAmount);
        await _unitOfWork.SaveChangesAsync();
        return payment;
    }

    public async Task<Payment> GetByIdAsync(int id)
    {
        var payment = await _unitOfWork.Payments.GetByIdAsync(id);
        if (payment == null) throw new KeyNotFoundException("Payment not found");
        return payment;
    }

    public async Task<PagedResult<Payment>> GetPageAsync(int pageNumber, int pageSize)
    {
        var query = _unitOfWork.Payments.GetAll.OrderBy(p => p.Id);
        return await query.ToPagedListAsync(pageNumber, pageSize);
    }
}