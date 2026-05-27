using cwiczenia6.DTOs;

namespace cwiczenia6.Services;

public interface IPatientService
{
    Task<IEnumerable<GetPatientDto>> GetPatients(string? search);
    Task<GetBedAssignmentDto> AssignBed(string pesel, NewBedAssignmentDto dto);
}
