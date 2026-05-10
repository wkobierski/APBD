using cwiczenia4.Models;

namespace cwiczenia4.Data;

public static class DataStore
{
    public static readonly List<Room> Rooms =
    [
        new() { Id = 1, Name = "Aula A", BuildingCode = "A", Floor = 0, Capacity = 120, HasProjector = true, IsActive = true },
        new() { Id = 2, Name = "Lab 101", BuildingCode = "A", Floor = 1, Capacity = 24, HasProjector = true, IsActive = true },
        new() { Id = 3, Name = "Sala 205", BuildingCode = "B", Floor = 2, Capacity = 30, HasProjector = false, IsActive = true },
        new() { Id = 4, Name = "Sala 301", BuildingCode = "B", Floor = 3, Capacity = 18, HasProjector = true, IsActive = true },
        new() { Id = 5, Name = "Magazyn", BuildingCode = "C", Floor = 0, Capacity = 8, HasProjector = false, IsActive = false }
    ];

    public static readonly List<Reservation> Reservations =
    [
        new() { Id = 1, RoomId = 1, OrganizerName = "Anna Kowalska", Topic = "Wykład inauguracyjny", Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(10, 30), Status = "confirmed" },
        new() { Id = 2, RoomId = 2, OrganizerName = "Jan Nowak", Topic = "Warsztat C#", Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(13, 0), Status = "confirmed" },
        new() { Id = 3, RoomId = 3, OrganizerName = "Piotr Wiśniewski", Topic = "Konsultacje", Date = new DateOnly(2026, 5, 11), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(15, 30), Status = "planned" },
        new() { Id = 4, RoomId = 4, OrganizerName = "Maria Lewandowska", Topic = "Egzamin", Date = new DateOnly(2026, 5, 12), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12, 0), Status = "confirmed" },
        new() { Id = 5, RoomId = 1, OrganizerName = "Tomasz Wójcik", Topic = "Spotkanie zespołu", Date = new DateOnly(2026, 5, 13), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(10, 0), Status = "cancelled" },
        new() { Id = 6, RoomId = 2, OrganizerName = "Katarzyna Kamińska", Topic = "Warsztat REST API", Date = new DateOnly(2026, 5, 14), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(15, 0), Status = "planned" }
    ];

    public static int NextRoomId() =>
        Rooms.Count == 0 ? 1 : Rooms.Max(r => r.Id) + 1;

    public static int NextReservationId() =>
        Reservations.Count == 0 ? 1 : Reservations.Max(r => r.Id) + 1;
}
