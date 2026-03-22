namespace cwiczenia2.Model.Devices;

public class Camera : Device
{
    public bool Has4KResolution { get; set; }
    public int HoursOnBattery { get; set; }

    public Camera(
        string name,
        AvailabilityStatus availabilityStatus,
        int ageInYears,
        int priceInUsd,
        bool has4KResolution,
        int hoursOnBattery
    ) : base(
        name,
        availabilityStatus,
        ageInYears,
        priceInUsd
    ) {
        Has4KResolution = has4KResolution;
        HoursOnBattery = hoursOnBattery;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | 4K: {Has4KResolution} | Battery: {HoursOnBattery}h";
    }
}