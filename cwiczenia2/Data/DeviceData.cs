using cwiczenia2.Model.Devices;
namespace cwiczenia2.Data;

public class DeviceData
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public AvailabilityStatus AvailabilityStatus { get; init; }
    public int AgeInYears { get; init; }
    public int PriceInUsd { get; init; }
    public string? Note { get; init; }
}
