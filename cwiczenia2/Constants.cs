namespace cwiczenia2;

public static class Constants
{
    public static readonly string[] AppActions =
    [
        "Add a user to the system",
        "Add a device of a given type",
        "Show a list of all devices with their statuses",
        "Show devices available to rent",
        "Rent a device to a user",
        "Return a device and calculate late return fee",
        "Mark device non-available",
        "Show all active rentals for user",
        "Show all expired rentals",
        "Generate a summary report",
    ];

    public const int RentalFreePeriodDays = 14;
    public const int LateFeePerDayPln = 5;
    public const int MaxStudentActiveRentals = 2;
    public const int MaxEmployeeActiveRentals = 5;
    public const string DataFilePath = "data.json";
    public const string CancelKeyword = "cancel";
    public static readonly string[] DeviceTypes = ["Camera", "Laptop", "Projector"];
}