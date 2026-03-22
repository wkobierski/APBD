namespace cwiczenia2.Model.Devices;

public class Projector : Device
{
    public int BrightnessLevel { get; }
    public int OptimalDistanceToScreen { get; }

    public Projector(
        string name,
        AvailabilityStatus availabilityStatus,
        int ageInYears,
        int priceInUsd,
        int brightnessLevel,
        int optimalDistanceToScreen
    ) : base(
        name,
        availabilityStatus,
        ageInYears,
        priceInUsd
    ) {
        BrightnessLevel = brightnessLevel;
        OptimalDistanceToScreen = optimalDistanceToScreen;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Brightness: {BrightnessLevel} | Distance: {OptimalDistanceToScreen}m";
    }
}