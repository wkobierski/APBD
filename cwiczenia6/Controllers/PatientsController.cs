using cwiczenia6.DTOs;
using cwiczenia6.Exceptions;
using cwiczenia6.Services;
using Microsoft.AspNetCore.Mvc;

namespace cwiczenia6.Controllers;

[Route("api/patients")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients([FromQuery] string? search)
    {
        var patients = await _patientService.GetPatients(search);
        return Ok(patients);
    }

    [HttpPost("{pesel}/bedassignments")]
    public async Task<IActionResult> AssignBed(string pesel, [FromBody] NewBedAssignmentDto body)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        try
        {
            var assignment = await _patientService.AssignBed(pesel, body);
            return CreatedAtAction(nameof(GetPatients), new { search = pesel }, assignment);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
