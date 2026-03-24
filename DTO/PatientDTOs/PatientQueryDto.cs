namespace HospitalManagementSystemAPIVersion.DTO.PatientDTOs;

public class PatientQueryDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;

    public string Name { get; set; } 
    public string? Search { get; set; } 
}