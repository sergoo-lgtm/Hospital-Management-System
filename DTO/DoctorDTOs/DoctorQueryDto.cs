namespace Hospital_Management_System.Model.DTO.DoctorDTOs;

public class DoctorQueryDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;

    public string Specialization { get; set; }
    public string Search { get; set; }

}