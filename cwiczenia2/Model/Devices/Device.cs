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

    public override string ToString()
    {
        return $"[{Id}] {Name} | Status: {AvailabilityStatus} | Age: {AgeInYearsInYears}y | Price: ${PriceInUsdInUsd}";
    }
}