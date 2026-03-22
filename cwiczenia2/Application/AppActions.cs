using cwiczenia2.Logic;
using cwiczenia2.Model.Devices;
using cwiczenia2.Model.Users;
namespace cwiczenia2.Application;

public static class AppActions
{
    private static void PrintCancelHint()
    {
        Console.WriteLine($"(Type '{Constants.CancelKeyword}' at any point to cancel)\n");
    }

    private static bool Cancelled(object? value)
    {
        if (value != null) return false;
        Console.WriteLine("Cancelled.\n");
        return true;
    }

    public static void AddUser()
    {
        Console.WriteLine("\n--- Add a User ---\n");
        PrintCancelHint();

        Console.WriteLine("Enter first name:");
        var firstNameInput = Utils.ReadCancellableString();
        if (Cancelled(firstNameInput)) return;
        var firstName = Utils.ToTitleCase(firstNameInput);

        Console.WriteLine("Enter last name:");
        var lastNameInput = Utils.ReadCancellableString();
        if (Cancelled(lastNameInput)) return;
        var lastName = Utils.ToTitleCase(lastNameInput);

        Console.WriteLine("\nChoose user type:");
        Utils.DisplayEnumOptions<UserType>();

        var typeChoice = Utils.ReadCancellableInt(1, Enum.GetValues<UserType>().Length);
        if (Cancelled(typeChoice)) return;
        var selectedType = Enum.GetValues<UserType>()[typeChoice.Value - 1];

        UserService.AddUser(firstName, lastName, selectedType);
        Console.WriteLine($"\nUser {firstName} {lastName} ({selectedType}) added successfully!\n");
    }

    public static void AddDevice()
    {
        Console.WriteLine("\n--- Add a Device ---\n");
        PrintCancelHint();

        Console.WriteLine("Choose device type:");
        for (var i = 0; i < Constants.DeviceTypes.Length; i++)
            Console.WriteLine($"{i + 1}. {Constants.DeviceTypes[i]}");

        var selectedType = Utils.ReadCancellableInt(1, Constants.DeviceTypes.Length);
        if (Cancelled(selectedType)) return;

        Console.WriteLine("\nEnter device name:");
        var nameInput = Utils.ReadCancellableString();
        if (Cancelled(nameInput)) return;
        var name = Utils.ToTitleCase(nameInput);

        Console.WriteLine("Choose availability status:");
        Utils.DisplayEnumOptions<AvailabilityStatus>();
        var statusChoice = Utils.ReadCancellableInt(1, Enum.GetValues<AvailabilityStatus>().Length);
        if (Cancelled(statusChoice)) return;
        var status = Enum.GetValues<AvailabilityStatus>()[statusChoice.Value - 1];

        Console.WriteLine("Enter age in years:");
        var age = Utils.ReadCancellableInt();
        if (Cancelled(age)) return;

        Console.WriteLine("Enter price in USD:");
        var price = Utils.ReadCancellableInt();
        if (Cancelled(price)) return;

        switch (selectedType.Value)
        {
            case 1:
                Console.WriteLine("Has 4K resolution? (y/n):");
                var has4KInput = Utils.ReadCancellableString();
                if (Cancelled(has4KInput)) return;
                var has4K = has4KInput.ToLower() is "y";
                Console.WriteLine("Hours on battery:");
                var hours = Utils.ReadCancellableInt();
                if (Cancelled(hours)) return;
                DeviceService.AddCamera(name, status, age.Value, price.Value, has4K, hours.Value);
                break;
            case 2:
                Console.WriteLine("Screen diagonal in inches:");
                var screen = Utils.ReadCancellableInt();
                if (Cancelled(screen)) return;
                Console.WriteLine("Weight in grams:");
                var weight = Utils.ReadCancellableInt();
                if (Cancelled(weight)) return;
                DeviceService.AddLaptop(name, status, age.Value, price.Value, screen.Value, weight.Value);
                break;
            case 3:
                Console.WriteLine("Brightness level:");
                var brightness = Utils.ReadCancellableInt();
                if (Cancelled(brightness)) return;
                Console.WriteLine("Optimal distance to screen (m):");
                var distance = Utils.ReadCancellableInt();
                if (Cancelled(distance)) return;
                DeviceService.AddProjector(name, status, age.Value, price.Value, brightness.Value, distance.Value);
                break;
        }

        Console.WriteLine($"\n{Constants.DeviceTypes[selectedType.Value - 1]} '{name}' added successfully!\n");
    }

    public static void ShowAllDevices()
    {
        Utils.DisplayList("All Devices", DeviceService.GetDevices(), "No devices found.");
    }

    public static void ShowAvailableDevices()
    {
        Utils.DisplayList("Available Devices", DeviceService.GetAvailableDevices(), "No available devices found.");
    }

    public static void RentDevice()
    {
        Console.WriteLine("\n--- Rent a Device ---\n");
        PrintCancelHint();

        var users = UserService.GetUsers();
        Console.WriteLine("Select a user (0 to add new):");
        foreach (var user in users)
            Console.WriteLine($"  {user}");
        Console.WriteLine("  0. Add new user\n");

        var userChoice = Utils.ReadCancellableInt(0, users.Count);
        if (Cancelled(userChoice)) return;
        if (userChoice == 0)
        {
            AddUser();
            users = UserService.GetUsers();
            userChoice = users.Count;
        }
        var selectedUser = users[userChoice.Value - 1];

        var devices = DeviceService.GetAvailableDevices();
        if (devices.Count == 0)
        {
            Console.WriteLine("\nNo available devices. Add a new one first.");
            AddDevice();
            devices = DeviceService.GetAvailableDevices();
            if (devices.Count == 0) { Console.WriteLine("Still no available devices.\n"); return; }
        }

        Console.WriteLine("\nSelect an available device (0 to add new):");
        for (var i = 0; i < devices.Count; i++)
            Console.WriteLine($"  {i + 1}. {devices[i]}");
        Console.WriteLine("  0. Add new device\n");

        var deviceChoice = Utils.ReadCancellableInt(0, devices.Count);
        if (Cancelled(deviceChoice)) return;
        if (deviceChoice == 0)
        {
            AddDevice();
            devices = DeviceService.GetAvailableDevices();
            Console.WriteLine("\nSelect an available device:");
            for (var i = 0; i < devices.Count; i++)
                Console.WriteLine($"  {i + 1}. {devices[i]}");
            Console.WriteLine();
            deviceChoice = Utils.ReadCancellableInt(1, devices.Count);
            if (Cancelled(deviceChoice)) return;
        }
        var selectedDevice = devices[deviceChoice.Value - 1];

        Console.WriteLine("\nEnter rental date (yyyy-MM-dd) or press Enter for today:");
        var dateInput = Utils.ReadCancellableString();
        if (Cancelled(dateInput)) return;
        var rentalDate = string.IsNullOrEmpty(dateInput) ? DateTime.Today : DateTime.Parse(dateInput);

        Console.WriteLine("Enter rental length in days:");
        var rentalLength = Utils.ReadCancellableInt(min: 1);
        if (Cancelled(rentalLength)) return;

        try
        {
            RentalService.CreateRental(selectedUser.Id, selectedDevice.Id, rentalDate, rentalLength.Value);
            Console.WriteLine($"\nDevice '{selectedDevice.Name}' rented to {selectedUser.FirstName} {selectedUser.LastName} for {rentalLength.Value} days.\n");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"\nError: {e.Message}\n");
        }
    }

    public static void ReturnDevice()
    {
        Console.WriteLine("\n--- Return a Device ---\n");
        PrintCancelHint();

        var activeRentals = RentalService.GetActiveRentals();

        if (activeRentals.Count == 0)
        {
            Console.WriteLine("No active rentals found.\n");
            return;
        }

        Console.WriteLine("Select a rental to return:");
        for (var i = 0; i < activeRentals.Count; i++)
            Console.WriteLine($"  {i + 1}. {activeRentals[i]}");
        Console.WriteLine();

        var choice = Utils.ReadCancellableInt(1, activeRentals.Count);
        if (Cancelled(choice)) return;
        var rental = activeRentals[choice.Value - 1];

        var fee = RentalService.ReturnDevice(rental.Id);

        Console.WriteLine($"\nDevice returned successfully!");
        if (fee > 0)
        {
            Console.WriteLine($"Late return fee: {fee} PLN");
            Console.WriteLine("Did the user pay the fee? y/n");
            var paid = Console.ReadLine()?.Trim().ToLower();
            if (paid is "y")
            {
                UserService.ClearFees(rental.UserId);
                Console.WriteLine("Fee paid. Balance cleared.");
            }
            else
            {
                Console.WriteLine("Fee outstanding. Balance remains on user account.");
            }
        }
        else
        {
            Console.WriteLine("Returned on time. No fee.");
        }
        Console.WriteLine();
    }

    public static void MarkDeviceUnavailable()
    {
        Console.WriteLine("\n--- Mark Device Unavailable ---\n");
        PrintCancelHint();

        var devices = DeviceService.GetDevices()
            .Where(d => d.AvailabilityStatus != AvailabilityStatus.Unavailable).ToList();

        if (devices.Count == 0)
        {
            Console.WriteLine("No devices to mark as unavailable.\n");
            return;
        }

        Console.WriteLine("Select a device:");
        for (var i = 0; i < devices.Count; i++)
            Console.WriteLine($"  {i + 1}. {devices[i]}");
        Console.WriteLine();

        var choice = Utils.ReadCancellableInt(1, devices.Count);
        if (Cancelled(choice)) return;
        var device = devices[choice.Value - 1];

        Console.WriteLine($"Enter a note (or press Enter to skip, '{Constants.CancelKeyword}' to cancel):");
        var note = Utils.ReadCancellableString();
        if (Cancelled(note)) return;

        DeviceService.MarkUnavailable(device.Id, string.IsNullOrEmpty(note) ? null : note);
        Console.WriteLine($"\nDevice '{device.Name}' marked as unavailable.\n");
    }

    public static void ShowActiveRentalsForUser()
    {
        Console.WriteLine("\n--- Active Rentals for User ---\n");

        var users = UserService.GetUsers();
        if (users.Count == 0)
        {
            Console.WriteLine("No users in the system.\n");
            return;
        }

        Console.WriteLine("Select a user:");
        for (var i = 0; i < users.Count; i++)
            Console.WriteLine($"  {i + 1}. {users[i]}");
        Console.WriteLine();

        var choice = Utils.ReadInt(1, users.Count);
        var user = users[choice - 1];

        var rentals = RentalService.GetActiveRentalsForUser(user.Id);

        if (rentals.Count == 0)
        {
            Console.WriteLine($"\nNo active rentals for {user.FirstName} {user.LastName}.\n");
            return;
        }

        Console.WriteLine($"\nActive rentals for {user.FirstName} {user.LastName}:\n");
        foreach (var rental in rentals)
            Console.WriteLine($"  {rental}");
        Console.WriteLine();
    }

    public static void ShowExpiredRentals()
    {
        Console.WriteLine("\n--- Expired Rentals ---\n");

        var rentals = RentalService.GetExpiredRentals();

        if (rentals.Count == 0)
        {
            Console.WriteLine("No expired rentals.\n");
            return;
        }

        foreach (var rental in rentals)
            Console.WriteLine($"  {rental}");
        Console.WriteLine();
    }

    public static void GenerateReport()
    {
        Console.WriteLine("\n========== RENTAL SHOP SUMMARY REPORT ==========\n");

        var users = UserService.GetUsers();
        Console.WriteLine($"Total users: {users.Count}");

        var devices = DeviceService.GetDevices();
        Console.WriteLine($"Total devices: {devices.Count}");

        Console.WriteLine("\n  Devices by status:");
        foreach (var group in devices.GroupBy(d => d.AvailabilityStatus))
            Console.WriteLine($"    {group.Key}: {group.Count()}");

        Console.WriteLine("\n  Devices by type:");
        foreach (var group in devices.GroupBy(d => d.GetType().Name))
            Console.WriteLine($"    {group.Key}: {group.Count()}");

        var allRentals = RentalService.GetAllRentals();
        var activeRentals = RentalService.GetActiveRentals();
        var expiredRentals = RentalService.GetExpiredRentals();

        Console.WriteLine($"\nTotal rentals: {allRentals.Count}");
        Console.WriteLine($"Active rentals: {activeRentals.Count}");
        Console.WriteLine($"Expired (overdue) rentals: {expiredRentals.Count}");

        var totalFees = RentalService.GetTotalLateFees();
        Console.WriteLine($"\nTotal late fees accumulated: {totalFees} PLN");

        if (allRentals.Count > 0)
        {
            var mostRentedDeviceId = allRentals
                .GroupBy(r => r.DeviceId)
                .OrderByDescending(g => g.Count())
                .First().Key;
            var mostRentedDevice = devices.First(d => d.Id == mostRentedDeviceId);
            Console.WriteLine($"Most rented device: {mostRentedDevice.Name} ({allRentals.Count(r => r.DeviceId == mostRentedDeviceId)} rentals)");

            var topUserId = allRentals
                .GroupBy(r => r.UserId)
                .OrderByDescending(g => g.Count())
                .First().Key;
            var topUser = users.First(u => u.Id == topUserId);
            Console.WriteLine($"User with most rentals: {topUser.FirstName} {topUser.LastName} ({allRentals.Count(r => r.UserId == topUserId)} rentals)");
        }

        Console.WriteLine("\n=================================================\n");
    }
}
