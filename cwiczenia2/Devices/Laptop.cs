namespace cwiczenia2.Devices;

public class Laptop : Device
{
    public int ScreenDiagonalInInches { get; set; }
    public int WeightInGrams { get; set; }

    public Laptop(
        string name, 
        string availabilityStatus, 
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
}