namespace HospitalManagementSystemAPIVersion.DTO.PaymentDTOs;

public class PaymentDto
{
    
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public int Amount { get; set; }
    public bool IsPaid { get; set; }
}