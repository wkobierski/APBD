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
}
