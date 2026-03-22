namespace cwiczenia2.Data;

public class RentalData
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public int DeviceId { get; init; }
    public DateTime RentalDate { get; init; }
    public int RentalLengthInDays { get; init; }
    public bool? ReturnedInTime { get; init; }
}
