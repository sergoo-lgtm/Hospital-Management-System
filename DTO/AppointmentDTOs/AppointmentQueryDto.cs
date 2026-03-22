namespace Hospital_Management_System.Model.DTO.AppointmentDTOs;

public class AppointmentQueryDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;

    public string Status { get; set; }
    public int? DoctorId { get; set; }
}