namespace cwiczenia2.Model.Rentals;

public class Rental
{
    private static int _id = 0;
    private static List<Rental> _rentals = new List<Rental>();
    public static IReadOnlyList<Rental> Rentals => _rentals;
    public int Id { get; set; }
    public int UserId { get; set; }
    public int DeviceId { get; set; }
    public DateTime RentalDate { get; set; }
    public int RentalLenghtInDays { get; set; }
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
        RentalLenghtInDays = rentalLenghtInDays;
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
        return $"[{Id}] User: {UserId} | Device: {DeviceId} | Date: {RentalDate:yyyy-MM-dd} | Days: {RentalLenghtInDays} | Returned in time: {ReturnedInTime?.ToString() ?? "N/A"}";
    }
}