namespace HospitalManagementSystemAPIVersion.DTO.AppointmentDTOs;

public class CreateAppointmentDto
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime Date { get; set; }
    public int Amount { get; set; }
}