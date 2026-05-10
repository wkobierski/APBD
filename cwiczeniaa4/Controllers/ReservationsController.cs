using cwiczenia4.Data;
using cwiczenia4.Models;
using Microsoft.AspNetCore.Mvc;

namespace cwiczenia4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        IEnumerable<Reservation> result = DataStore.Reservations;

        if (date.HasValue)
            result = result.Where(r => r.Date == date.Value);

        if (!string.IsNullOrWhiteSpace(status))
            result = result.Where(r => string.Equals(r.Status, status, StringComparison.OrdinalIgnoreCase));

        if (roomId.HasValue)
            result = result.Where(r => r.RoomId == roomId.Value);

        return Ok(result.ToList());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> GetById([FromRoute] int id)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation is null)
            return NotFound(new { message = $"Rezerwacja o id {id} nie istnieje." });
        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult<Reservation> Create([FromBody] Reservation reservation)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (room is null)
            return NotFound(new { message = $"Sala o id {reservation.RoomId} nie istnieje." });

        if (!room.IsActive)
            return Conflict(new { message = "Sala jest oznaczona jako nieaktywna." });

        if (HasOverlap(reservation, excludeId: null))
            return Conflict(new { message = "Rezerwacja koliduje czasowo z istniejącą rezerwacją tej samej sali." });

        reservation.Id = DataStore.NextReservationId();
        DataStore.Reservations.Add(reservation);
        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Reservation> Update([FromRoute] int id, [FromBody] Reservation reservation)
    {
        var existing = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (existing is null)
            return NotFound(new { message = $"Rezerwacja o id {id} nie istnieje." });

        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (room is null)
            return NotFound(new { message = $"Sala o id {reservation.RoomId} nie istnieje." });

        if (!room.IsActive)
            return Conflict(new { message = "Sala jest oznaczona jako nieaktywna." });

        if (HasOverlap(reservation, excludeId: id))
            return Conflict(new { message = "Rezerwacja koliduje czasowo z istniejącą rezerwacją tej samej sali." });

        existing.RoomId = reservation.RoomId;
        existing.OrganizerName = reservation.OrganizerName;
        existing.Topic = reservation.Topic;
        existing.Date = reservation.Date;
        existing.StartTime = reservation.StartTime;
        existing.EndTime = reservation.EndTime;
        existing.Status = reservation.Status;

        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation is null)
            return NotFound(new { message = $"Rezerwacja o id {id} nie istnieje." });

        DataStore.Reservations.Remove(reservation);
        return NoContent();
    }

    private static bool HasOverlap(Reservation candidate, int? excludeId)
    {
        return DataStore.Reservations.Any(r =>
            r.RoomId == candidate.RoomId &&
            r.Date == candidate.Date &&
            (!excludeId.HasValue || r.Id != excludeId.Value) &&
            candidate.StartTime < r.EndTime &&
            r.StartTime < candidate.EndTime);
    }
}
