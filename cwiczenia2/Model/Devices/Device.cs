namespace cwiczenia2.Model.Devices;

public abstract class Device
{
    private static int _id = 0;
    public int Id { get; set; }
    public string Name { get; set; }
    public string AvailabilityStatus { get; set; }
    public int AgeInYearsInYears { get; set; }
    public int PriceInUsdInUsd { get; set; }

    protected Device(
        string name, 
        string availabilityStatus, 
        int ageInYears, 
        int priceInUsd
    ) {
        Id = ++_id;
        Name = name;
        AvailabilityStatus = availabilityStatus;
        AgeInYearsInYears = ageInYears;
        PriceInUsdInUsd = priceInUsd;
    }
}