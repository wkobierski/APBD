namespace cwiczenia2;

public class Rental
{
    private static int _id = 0;
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
    }
}