namespace HospitalManagementSystemAPIVersion.DTO.AppointmentDTOs;

public class AppointmentDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int? PaymentId { get; set; }       
    public int? PrescriptionId { get; set; }  
}