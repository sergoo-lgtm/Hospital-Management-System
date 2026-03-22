namespace Hospital_Management_System.Model.DTO.PrescriptionDTOs;

public class CreatePrescriptionDto
{
    
    public int AppointmentId { get; set; }
    public string Notes { get; set; }
    public string Medications { get; set; }
}