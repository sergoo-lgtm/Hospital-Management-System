using Hospital_Management_System.Model;
using Hospital_Management_System.UnitOfWork;
using Hospital_Management_System.Model.DTO.PaymentDTOs;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.Model.Service;

public class PaymentService
{
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ================= Create Payment =================
    public async Task CreateAsync(int appointmentId, int amount)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId);

        if (appointment == null)
            throw new Exception("Appointment not found");

        var payment = new Payment(appointmentId, amount);
        appointment.AssignPayment(payment);

        await _unitOfWork.Payments.AddAsync(payment);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Pay =================
    public async Task PayAsync(PayDto dto)
    {
        var payment = await _unitOfWork.Payments.Query
            .FirstOrDefaultAsync(p => p.AppointmentId == dto.AppointmentId);

        if (payment == null)
            throw new Exception("Payment not found");

        var isPaid = payment.Pay(dto.AppointmentId);

        if (!isPaid)
            throw new Exception("Payment failed");

        await _unitOfWork.Payments.UpdateAsync(payment);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Update Amount =================
    public async Task UpdateAmountAsync(UpdatePaymentDto dto)
    {
        var payment = await _unitOfWork.Payments.GetByIdAsync(dto.PaymentId);

        if (payment == null)
            throw new Exception("Payment not found");

        payment.UpdateAmount(dto.NewAmount);

        await _unitOfWork.Payments.UpdateAsync(payment);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Get By Id =================
    public async Task<Payment> GetByIdAsync(int id)
    {
        var payment = await _unitOfWork.Payments.Query
            .Include(p => p.Appointment)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (payment == null)
            throw new Exception("Payment not found");

        return payment;
    }

    // ================= Delete =================
    public async Task DeleteAsync(int id)
    {
        var payment = await _unitOfWork.Payments.GetByIdAsync(id);

        if (payment == null)
            throw new Exception("Payment not found");

        await _unitOfWork.Payments.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    // ================= Pagination =================
    public async Task<List<Payment>> GetPageAsync(int pageNumber, int pageSize = 5)
    {
        return await _unitOfWork.Payments.Query
            .Include(p => p.Appointment)
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    // ================= Filter Paid/Unpaid =================
    public async Task<List<Payment>> GetPaidAsync(bool isPaid)
    {
        return await _unitOfWork.Payments.Query
            .Where(p => p.IsPaid == isPaid)
            .ToListAsync();
    }
}