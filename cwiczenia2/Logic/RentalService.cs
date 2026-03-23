using cwiczenia2.Model.Devices;
using cwiczenia2.Model.Rentals;
using cwiczenia2.Model.Users;
namespace cwiczenia2.Logic;

public static class RentalService
{
    public static void CreateRental(int userId, int deviceId, DateTime rentalDate, int rentalLengthInDays)
    {
        var user = User.Users.First(u => u.Id == userId);

        var maxRentals = user.UserType == UserType.Student
            ? Constants.MaxStudentActiveRentals
            : Constants.MaxEmployeeActiveRentals;

        var activeCount = GetActiveRentalsForUser(userId).Count;
        if (activeCount >= maxRentals)
            throw new InvalidOperationException(
                $"{user.UserType}s can have a maximum of {maxRentals} active rentals.");

        var device = Device.Devices.First(d => d.Id == deviceId);
        if (device.AvailabilityStatus != AvailabilityStatus.Available)
            throw new InvalidOperationException(
                $"Device '{device.Name}' is not available for rent (status: {device.AvailabilityStatus}).");
        device.AvailabilityStatus = AvailabilityStatus.Rented;
        new Rental(userId, deviceId, rentalDate, rentalLengthInDays, null);
    }

    public static List<Rental> GetActiveRentals()
    {
        return Rental.Rentals.Where(r => r.ReturnedInTime == null).ToList();
    }

    public static List<Rental> GetActiveRentalsForUser(int userId)
    {
        return Rental.Rentals.Where(r => r.UserId == userId && r.ReturnedInTime == null).ToList();
    }

    public static List<Rental> GetExpiredRentals()
    {
        return Rental.Rentals
            .Where(r => r.ReturnedInTime == null && (DateTime.Today - r.RentalDate).Days > r.RentalLengthInDays)
            .ToList();
    }

    public static IReadOnlyList<Rental> GetAllRentals()
    {
        return Rental.Rentals;
    }

    public static int GetTotalLateFees()
    {
        return User.Users.Sum(u => u.TotalLateFees);
    }

    public static int ReturnDevice(int rentalId)
    {
        var rental = Rental.Rentals.First(r => r.Id == rentalId);
        var device = Device.Devices.First(d => d.Id == rental.DeviceId);
        var user = User.Users.First(u => u.Id == rental.UserId);

        var totalDays = (DateTime.Today - rental.RentalDate).Days;
        var lateDays = Math.Max(0, totalDays - rental.RentalLengthInDays);
        var fee = lateDays * Constants.LateFeePerDayPln;

        rental.ReturnedInTime = lateDays == 0;
        device.AvailabilityStatus = AvailabilityStatus.Available;
        user.TotalLateFees += fee;

        return fee;
    }
}
