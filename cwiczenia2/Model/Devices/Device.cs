namespace cwiczenia2.Model.Devices;

public abstract class Device
{
    private static int _id = 0;
    private static List<Device> _devices = new List<Device>();
    public static IReadOnlyList<Device> Devices => _devices;
    public int Id { get; set; }
    public string Name { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }
    public int AgeInYearsInYears { get; set; }
    public int PriceInUsdInUsd { get; set; }
    public string? Note { get; set; }

    protected Device(
        string name, 
        AvailabilityStatus availabilityStatus,
        int ageInYears, 
        int priceInUsd
    ) {
        Id = ++_id;
        Name = name;
        AvailabilityStatus = availabilityStatus;
        AgeInYearsInYears = ageInYears;
        PriceInUsdInUsd = priceInUsd;
        _devices.Add(this);
    }

    public static void Reset()
    {
        _devices.Clear();
        _id = 0;
    }

    public static void SetIdCounter(int value)
    {
        _id = value;
    }

    public override string ToString()
    {
        var result = $"[{Id}] {Name} | Status: {AvailabilityStatus} | Age: {AgeInYearsInYears}y | Price: ${PriceInUsdInUsd}";
        if (!string.IsNullOrEmpty(Note))
            result += $" | Note: {Note}";
        return result;
    }
}