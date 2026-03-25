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

    // الدفع
    public async Task<bool> PayAsync(PayDto dto)
    {
        if (dto == null) 
            throw new ArgumentNullException(nameof(dto), "DTO must be provided");

        var payment = await _unitOfWork.Payments.GetAll
            .FirstOrDefaultAsync(p => p.AppointmentId == dto.AppointmentId);

        if (payment == null) 
            throw new KeyNotFoundException("Payment not found");

        var result = payment.Pay(dto.AppointmentId);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    // تحديث المبلغ
    public async Task<PaymentDto> UpdateAmountAsync(UpdatePaymentDto dto)
    {
        if (dto == null) 
            throw new ArgumentNullException(nameof(dto), "DTO must be provided");

        var payment = await _unitOfWork.Payments.GetByIdAsync(dto.PaymentId);
        if (payment == null) 
            throw new KeyNotFoundException("Payment not found");

        payment.UpdateAmount(dto.NewAmount);
        await _unitOfWork.SaveChangesAsync();

        return new PaymentDto
        {
            Id = payment.Id,
            AppointmentId = payment.AppointmentId,
            Amount = payment.Amount,
            IsPaid = payment.IsPaid
        };
    }

    // جلب الدفع بالـ Id
    public async Task<PaymentDto> GetByIdAsync(int id)
    {
        var payment = await _unitOfWork.Payments.GetByIdAsync(id);
        if (payment == null) 
            throw new KeyNotFoundException("Payment not found");

        return new PaymentDto
        {
            Id = payment.Id,
            AppointmentId = payment.AppointmentId,
            Amount = payment.Amount,
            IsPaid = payment.IsPaid
        };
    }

    // Pagination
    public async Task<PagedResult<PaymentDto>> GetPageAsync(int pageNumber, int pageSize)
    {
        var mappedQuery = _unitOfWork.Payments.GetAll
            .OrderBy(p => p.Id)
            .Select(p => new PaymentDto
            {
                Id = p.Id,
                AppointmentId = p.AppointmentId,
                Amount = p.Amount,
                IsPaid = p.IsPaid
            });

        var totalCount = await mappedQuery.CountAsync();
        var items = await mappedQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<PaymentDto>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}