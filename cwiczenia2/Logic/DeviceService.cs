using cwiczenia2.Model.Devices;

namespace cwiczenia2.Logic;

public static class DeviceService
{
    public static void AddCamera(string name, string status, int age, int price, bool has4K, int hoursOnBattery)
    {
        new Camera(name, status, age, price, has4K, hoursOnBattery);
    }

    public static void AddLaptop(string name, string status, int age, int price, int screenDiagonal, int weight)
    {
        new Laptop(name, status, age, price, screenDiagonal, weight);
    }

    public static void AddProjector(string name, string status, int age, int price, int brightness, int distance)
    {
        new Projector(name, status, age, price, brightness, distance);
    }

    public static IReadOnlyList<Device> GetDevices()
    {
        return Device.Devices;
    }
}
