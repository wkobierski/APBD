namespace cwiczenia2.Model.Rentals;

public class Rental
{
    private static int _id = 0;
    private static readonly List<Rental> _rentals = [];
    public static IReadOnlyList<Rental> Rentals => _rentals;
    public int Id { get; init; }
    public int UserId { get; }
    public int DeviceId { get; }
    public DateTime RentalDate { get; }
    public int RentalLengthInDays { get; }
    public bool? ReturnedInTime { get; set; }

    public Rental(
        int userId,
        int deviceId,
        DateTime rentalDate,
        int rentalLenghtInDays,
        bool? returnedInTime
    ) {
        Id = ++_id;
        UserId = userId;
        DeviceId = deviceId;
        RentalDate = rentalDate;
        RentalLengthInDays = rentalLenghtInDays;
        ReturnedInTime = returnedInTime;
        _rentals.Add(this);
    }

    public static void Reset()
    {
        _rentals.Clear();
        _id = 0;
    }

    public static void SetIdCounter(int value)
    {
        _id = value;
    }

    public override string ToString()
    {
        return $"[{Id}] User: {UserId} | Device: {DeviceId} | Date: {RentalDate:yyyy-MM-dd} | Days: {RentalLengthInDays} | Returned in time: {ReturnedInTime?.ToString() ?? "N/A"}";
    }
}