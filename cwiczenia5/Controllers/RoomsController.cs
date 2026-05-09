using cwiczenia4.Data;
using cwiczenia4.Models;
using Microsoft.AspNetCore.Mvc;

namespace cwiczenia4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        IEnumerable<Room> result = DataStore.Rooms;

        if (minCapacity.HasValue)
            result = result.Where(r => r.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            result = result.Where(r => r.HasProjector == hasProjector.Value);

        if (activeOnly == true)
            result = result.Where(r => r.IsActive);

        return Ok(result.ToList());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Room> GetById([FromRoute] int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room is null)
            return NotFound(new { message = $"Sala o id {id} nie istnieje." });
        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<Room>> GetByBuilding([FromRoute] string buildingCode)
    {
        var rooms = DataStore.Rooms
            .Where(r => string.Equals(r.BuildingCode, buildingCode, StringComparison.OrdinalIgnoreCase))
            .ToList();
        return Ok(rooms);
    }

    [HttpPost]
    public ActionResult<Room> Create([FromBody] Room room)
    {
        room.Id = DataStore.NextRoomId();
        DataStore.Rooms.Add(room);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Room> Update([FromRoute] int id, [FromBody] Room room)
    {
        var existing = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (existing is null)
            return NotFound(new { message = $"Sala o id {id} nie istnieje." });

        existing.Name = room.Name;
        existing.BuildingCode = room.BuildingCode;
        existing.Floor = room.Floor;
        existing.Capacity = room.Capacity;
        existing.HasProjector = room.HasProjector;
        existing.IsActive = room.IsActive;

        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room is null)
            return NotFound(new { message = $"Sala o id {id} nie istnieje." });

        var today = DateOnly.FromDateTime(DateTime.Today);
        var hasFutureReservations = DataStore.Reservations
            .Any(r => r.RoomId == id && r.Date >= today);

        if (hasFutureReservations)
            return Conflict(new { message = "Nie można usunąć sali z przyszłymi rezerwacjami." });

        DataStore.Rooms.Remove(room);
        return NoContent();
    }
}
