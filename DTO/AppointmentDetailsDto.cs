namespace HospitalManagementSystemAPIVersion.DTO;

public class AppointmentDetailsDto
{
    public int AppointmentId { get; set; }
    public string PatientName { get; set; }
    public string DoctorName { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string AppointmentStatus { get; set; }
}