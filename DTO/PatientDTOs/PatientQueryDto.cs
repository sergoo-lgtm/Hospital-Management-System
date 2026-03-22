namespace Hospital_Management_System.Model.DTO.patientDTOs;

public class PatientQueryDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;

    public string Name { get; set; } // filter
    public string Search { get; set; }

}