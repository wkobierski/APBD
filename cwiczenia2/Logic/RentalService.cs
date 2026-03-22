using cwiczenia2.Model.Devices;
using cwiczenia2.Model.Rentals;

namespace cwiczenia2.Logic;

public static class RentalService
{
    public static void CreateRental(int userId, int deviceId, DateTime rentalDate, int rentalLengthInDays)
    {
        var device = Device.Devices.First(d => d.Id == deviceId);
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
            .Where(r => r.ReturnedInTime == null && (DateTime.Today - r.RentalDate).Days > Constants.RentalFreePeriodDays)
            .ToList();
    }

    public static int ReturnDevice(int rentalId)
    {
        var rental = Rental.Rentals.First(r => r.Id == rentalId);
        var device = Device.Devices.First(d => d.Id == rental.DeviceId);

        var totalDays = (DateTime.Today - rental.RentalDate).Days;
        var lateDays = Math.Max(0, totalDays - Constants.RentalFreePeriodDays);
        var fee = lateDays * Constants.LateFeePerDayPln;

        rental.ReturnedInTime = lateDays == 0;
        device.AvailabilityStatus = AvailabilityStatus.Available;

        return fee;
    }
}
