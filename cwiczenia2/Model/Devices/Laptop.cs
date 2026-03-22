namespace cwiczenia2.Model.Devices;

public class Laptop : Device
{
    public int ScreenDiagonalInInches { get; set; }
    public int WeightInGrams { get; set; }

    public Laptop(
        string name, 
        AvailabilityStatus availabilityStatus,
        int ageInYears, 
        int priceInUsd, 
        int screenDiagonalInInches, 
        int weightInGrams
    ) : base(
        name,  
        availabilityStatus, 
        ageInYears, 
        priceInUsd
    ) {
        ScreenDiagonalInInches = screenDiagonalInInches;
        WeightInGrams = weightInGrams;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Screen: {ScreenDiagonalInInches}\" | Weight: {WeightInGrams}g";
    }
}