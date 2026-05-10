using cwiczenia5.DTOs;
using cwiczenia5.Exceptions;
using cwiczenia5.Services;
using Microsoft.AspNetCore.Mvc;

namespace cwiczenia5.Controllers;

[Route("api/pcs")]
[ApiController]
public class PcController : ControllerBase
{
    private readonly IPcService _pcService;

    public PcController(IPcService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPcs()
    {
        var pcs = await _pcService.GetAllPcs();
        return Ok(pcs);
    }

    [HttpGet("{id}/components")]
    public async Task<IActionResult> GetPcComponents(int id)
    {
        try
        {
            var components = await _pcService.GetPcComponents(id);
            return Ok(components);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPc([FromBody] NewPcDto body)
    {
        var newPc = await _pcService.AddPc(body);
        return CreatedAtAction(nameof(GetPcComponents), new { id = newPc.Id }, newPc);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePc(int id, [FromBody] NewPcDto body)
    {
        try
        {
            var updatedPc = await _pcService.UpdatePc(id, body);
            return Ok(updatedPc);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePc(int id)
    {
        try
        {
            await _pcService.DeletePc(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}