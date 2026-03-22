using System.Text.Json;
using cwiczenia2.Model.Devices;
using cwiczenia2.Model.Rentals;
using cwiczenia2.Model.Users;

namespace cwiczenia2.Data;

public class DataStore
{
    public List<UserData> Users { get; init; } = [];
    public List<CameraData> Cameras { get; init; } = [];
    public List<LaptopData> Laptops { get; init; } = [];
    public List<ProjectorData> Projectors { get; init; } = [];
    public List<RentalData> Rentals { get; init; } = [];

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
                AgeInYears = c.AgeInYears, PriceInUsd = c.PriceInUsd, Note = c.Note,
                Has4KResolution = c.Has4KResolution, HoursOnBattery = c.HoursOnBattery
            }).ToList(),

            Laptops = Device.Devices.OfType<Laptop>().Select(l => new LaptopData
            {
                Id = l.Id, Name = l.Name, AvailabilityStatus = l.AvailabilityStatus,
                AgeInYears = l.AgeInYears, PriceInUsd = l.PriceInUsd, Note = l.Note,
                ScreenDiagonalInInches = l.ScreenDiagonalInInches, WeightInGrams = l.WeightInGrams
            }).ToList(),

            Projectors = Device.Devices.OfType<Projector>().Select(p => new ProjectorData
            {
                Id = p.Id, Name = p.Name, AvailabilityStatus = p.AvailabilityStatus,
                AgeInYears = p.AgeInYears, PriceInUsd = p.PriceInUsd, Note = p.Note,
                BrightnessLevel = p.BrightnessLevel, OptimalDistanceToScreen = p.OptimalDistanceToScreen
            }).ToList(),

            Rentals = Rental.Rentals.Select(r => new RentalData
            {
                Id = r.Id, UserId = r.UserId, DeviceId = r.DeviceId,
                RentalDate = r.RentalDate, RentalLengthInDays = r.RentalLengthInDays,
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
            new User(u.FirstName, u.LastName, u.UserType)
            {
                Id = u.Id,
                TotalLateFees = u.TotalLateFees
            };
        }
        if (store.Users.Count > 0)
            User.SetIdCounter(store.Users.Max(u => u.Id));

        foreach (var c in store.Cameras)
        {
            new Camera(c.Name, c.AvailabilityStatus, c.AgeInYears, c.PriceInUsd, c.Has4KResolution, c.HoursOnBattery)
            {
                Id = c.Id, Note = c.Note
            };
        }
        foreach (var l in store.Laptops)
        {
            new Laptop(l.Name, l.AvailabilityStatus, l.AgeInYears, l.PriceInUsd, l.ScreenDiagonalInInches, l.WeightInGrams)
            {
                Id = l.Id, Note = l.Note
            };
        }
        foreach (var p in store.Projectors)
        {
            new Projector(p.Name, p.AvailabilityStatus, p.AgeInYears, p.PriceInUsd, p.BrightnessLevel, p.OptimalDistanceToScreen)
            {
                Id = p.Id, Note = p.Note
            };
        }
        var allDeviceIds = store.Cameras.Select(c => c.Id)
            .Concat(store.Laptops.Select(l => l.Id))
            .Concat(store.Projectors.Select(p => p.Id))
            .ToList();
        if (allDeviceIds.Count > 0)
            Device.SetIdCounter(allDeviceIds.Max());

        foreach (var r in store.Rentals)
        {
            new Rental(r.UserId, r.DeviceId, r.RentalDate, r.RentalLengthInDays, r.ReturnedInTime)
            {
                Id = r.Id
            };
        }
        if (store.Rentals.Count > 0)
            Rental.SetIdCounter(store.Rentals.Max(r => r.Id));
    }
}
