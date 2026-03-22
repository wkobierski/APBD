using cwiczenia2.Logic;
using cwiczenia2.Model.Users;

namespace cwiczenia2.Application;

public static class AppActions
{
    public static void AddUser()
    {
        Console.WriteLine("\n--- Add a User ---\n");

        Console.WriteLine("Enter first name:");
        var firstName = Utils.ToTitleCase(Console.ReadLine()!);

        Console.WriteLine("Enter last name:");
        var lastName = Utils.ToTitleCase(Console.ReadLine()!);

        Console.WriteLine("\nChoose user type:");
        var userTypes = Enum.GetValues<UserType>();
        for (var i = 0; i < userTypes.Length; i++)
            Console.WriteLine($"{i + 1}. {userTypes[i]}");

        var selectedType = userTypes[Utils.ReadInt(1, userTypes.Length) - 1];

        UserService.AddUser(firstName!, lastName!, selectedType);
        Console.WriteLine($"\nUser {firstName} {lastName} ({selectedType}) added successfully!\n");
    }

    public static void AddDevice()
    {
        Console.WriteLine("\n--- Add a Device ---\n");

        Console.WriteLine("Choose device type:");
        string[] deviceTypes = ["Camera", "Laptop", "Projector"];
        for (var i = 0; i < deviceTypes.Length; i++)
            Console.WriteLine($"{i + 1}. {deviceTypes[i]}");

        var selectedType = Utils.ReadInt(1, deviceTypes.Length);

        Console.WriteLine("\nEnter device name:");
        var name = Utils.ToTitleCase(Console.ReadLine()!);

        Console.WriteLine("Enter availability status:");
        var status = Utils.ToTitleCase(Console.ReadLine()!);

        Console.WriteLine("Enter age in years:");
        var age = Utils.ReadInt();

        Console.WriteLine("Enter price in USD:");
        var price = Utils.ReadInt();

        switch (selectedType)
        {
            case 1:
                Console.WriteLine("Has 4K resolution? (y/n):");
                var has4K = Console.ReadLine()!.Trim().ToLower() is "y";
                Console.WriteLine("Hours on battery:");
                var hours = Utils.ReadInt();
                DeviceService.AddCamera(name, status, age, price, has4K, hours);
                break;
            case 2:
                Console.WriteLine("Screen diagonal in inches:");
                var screen = Utils.ReadInt();
                Console.WriteLine("Weight in grams:");
                var weight = Utils.ReadInt();
                DeviceService.AddLaptop(name, status, age, price, screen, weight);
                break;
            case 3:
                Console.WriteLine("Brightness level:");
                var brightness = Utils.ReadInt();
                Console.WriteLine("Optimal distance to screen (m):");
                var distance = Utils.ReadInt();
                DeviceService.AddProjector(name, status, age, price, brightness, distance);
                break;
        }

        Console.WriteLine($"\n{deviceTypes[selectedType - 1]} '{name}' added successfully!\n");
    }

}
