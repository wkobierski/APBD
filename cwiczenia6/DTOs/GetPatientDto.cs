namespace cwiczenia6.DTOs;

public class GetPatientDto
{
    public string Pesel { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int Age { get; set; }
    public string Sex { get; set; } = null!;
    public List<GetAdmissionDto> Admissions { get; set; } = [];
    public List<GetBedAssignmentDto> BedAssignments { get; set; } = [];
}
