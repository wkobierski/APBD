namespace cwiczenia2.Model.Devices;

public class Projector : Device
{
    public int BrightnessLevel { get; set; }
    public int OptimalDistanceToScreen { get; set; }

    public Projector(
        string name,
        string availabilityStatus,
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