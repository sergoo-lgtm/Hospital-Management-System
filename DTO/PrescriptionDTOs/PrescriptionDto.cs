namespace HospitalManagementSystemAPIVersion.DTO.PrescriptionDTOs;

public class PrescriptionDto
{
    public int Id { get; set; }                  
    public int AppointmentId { get; set; }      
    public string Notes { get; set; }          
    public string Medications { get; set; }     
}