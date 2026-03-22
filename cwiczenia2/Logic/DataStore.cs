using System.Text.Json;
using cwiczenia2.Model.Devices;
using cwiczenia2.Model.Rentals;
using cwiczenia2.Model.Users;

namespace cwiczenia2.Logic;

public class DataStore
{
    public List<UserData> Users { get; set; } = [];
    public List<CameraData> Cameras { get; set; } = [];
    public List<LaptopData> Laptops { get; set; } = [];
    public List<ProjectorData> Projectors { get; set; } = [];
    public List<RentalData> Rentals { get; set; } = [];

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public static void SaveData()
    {
        var store = new DataStore
        {
            Users = User.Users.Select(u => new UserData
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserType = u.UserType,
                TotalLateFees = u.TotalLateFees
            }).ToList(),

            Cameras = Device.Devices.OfType<Camera>().Select(c => new CameraData
            {
                Id = c.Id, Name = c.Name, AvailabilityStatus = c.AvailabilityStatus,
                AgeInYears = c.AgeInYearsInYears, PriceInUsd = c.PriceInUsdInUsd, Note = c.Note,
                Has4KResolution = c.Has4KResolution, HoursOnBattery = c.HoursOnBattery
            }).ToList(),

            Laptops = Device.Devices.OfType<Laptop>().Select(l => new LaptopData
            {
                Id = l.Id, Name = l.Name, AvailabilityStatus = l.AvailabilityStatus,
                AgeInYears = l.AgeInYearsInYears, PriceInUsd = l.PriceInUsdInUsd, Note = l.Note,
                ScreenDiagonalInInches = l.ScreenDiagonalInInches, WeightInGrams = l.WeightInGrams
            }).ToList(),

            Projectors = Device.Devices.OfType<Projector>().Select(p => new ProjectorData
            {
                Id = p.Id, Name = p.Name, AvailabilityStatus = p.AvailabilityStatus,
                AgeInYears = p.AgeInYearsInYears, PriceInUsd = p.PriceInUsdInUsd, Note = p.Note,
                BrightnessLevel = p.BrightnessLevel, OptimalDistanceToScreen = p.OptimalDistanceToScreen
            }).ToList(),

            Rentals = Rental.Rentals.Select(r => new RentalData
            {
                Id = r.Id, UserId = r.UserId, DeviceId = r.DeviceId,
                RentalDate = r.RentalDate, RentalLengthInDays = r.RentalLenghtInDays,
                ReturnedInTime = r.ReturnedInTime
            }).ToList()
        };

        var json = JsonSerializer.Serialize(store, JsonOptions);
        File.WriteAllText(Constants.DataFilePath, json);
    }

    public static void LoadData()
    {
        if (!File.Exists(Constants.DataFilePath))
            return;

        var json = File.ReadAllText(Constants.DataFilePath);
        var store = JsonSerializer.Deserialize<DataStore>(json, JsonOptions);
        if (store == null) return;

        User.Reset();
        Device.Reset();
        Rental.Reset();

        foreach (var u in store.Users)
        {
            var user = new User(u.FirstName, u.LastName, u.UserType);
            user.Id = u.Id;
            user.TotalLateFees = u.TotalLateFees;
        }
        if (store.Users.Count > 0)
            User.SetIdCounter(store.Users.Max(u => u.Id));

        foreach (var c in store.Cameras)
        {
            var camera = new Camera(c.Name, c.AvailabilityStatus, c.AgeInYears, c.PriceInUsd, c.Has4KResolution, c.HoursOnBattery);
            camera.Id = c.Id;
            camera.Note = c.Note;
        }
        foreach (var l in store.Laptops)
        {
            var laptop = new Laptop(l.Name, l.AvailabilityStatus, l.AgeInYears, l.PriceInUsd, l.ScreenDiagonalInInches, l.WeightInGrams);
            laptop.Id = l.Id;
            laptop.Note = l.Note;
        }
        foreach (var p in store.Projectors)
        {
            var projector = new Projector(p.Name, p.AvailabilityStatus, p.AgeInYears, p.PriceInUsd, p.BrightnessLevel, p.OptimalDistanceToScreen);
            projector.Id = p.Id;
            projector.Note = p.Note;
        }
        var allDeviceIds = store.Cameras.Select(c => c.Id)
            .Concat(store.Laptops.Select(l => l.Id))
            .Concat(store.Projectors.Select(p => p.Id));
        if (allDeviceIds.Any())
            Device.SetIdCounter(allDeviceIds.Max());

        foreach (var r in store.Rentals)
        {
            var rental = new Rental(r.UserId, r.DeviceId, r.RentalDate, r.RentalLengthInDays, r.ReturnedInTime);
            rental.Id = r.Id;
        }
        if (store.Rentals.Count > 0)
            Rental.SetIdCounter(store.Rentals.Max(r => r.Id));
    }
}

// DTOs for serialization
public class UserData
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public UserType UserType { get; set; }
    public int TotalLateFees { get; set; }
}

public class DeviceData
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public AvailabilityStatus AvailabilityStatus { get; set; }
    public int AgeInYears { get; set; }
    public int PriceInUsd { get; set; }
    public string? Note { get; set; }
}

public class CameraData : DeviceData
{
    public bool Has4KResolution { get; set; }
    public int HoursOnBattery { get; set; }
}

public class LaptopData : DeviceData
{
    public int ScreenDiagonalInInches { get; set; }
    public int WeightInGrams { get; set; }
}

public class ProjectorData : DeviceData
{
    public int BrightnessLevel { get; set; }
    public int OptimalDistanceToScreen { get; set; }
}

public class RentalData
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int DeviceId { get; set; }
    public DateTime RentalDate { get; set; }
    public int RentalLengthInDays { get; set; }
    public bool? ReturnedInTime { get; set; }
}
