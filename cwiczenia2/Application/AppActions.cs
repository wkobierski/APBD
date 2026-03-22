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

    public static void AddUser()
    {
        Console.WriteLine("\n--- Add a User ---\n");
        PrintCancelHint();

        Console.WriteLine("Enter first name:");
        var firstNameInput = Utils.ReadCancellableString();
        if (firstNameInput == null) { Console.WriteLine("Cancelled.\n"); return; }
        var firstName = Utils.ToTitleCase(firstNameInput);

        Console.WriteLine("Enter last name:");
        var lastNameInput = Utils.ReadCancellableString();
        if (lastNameInput == null) { Console.WriteLine("Cancelled.\n"); return; }
        var lastName = Utils.ToTitleCase(lastNameInput);

        Console.WriteLine("\nChoose user type:");
        var userTypes = Enum.GetValues<UserType>();
        for (var i = 0; i < userTypes.Length; i++)
            Console.WriteLine($"{i + 1}. {userTypes[i]}");

        var typeChoice = Utils.ReadCancellableInt(1, userTypes.Length);
        if (typeChoice == null) { Console.WriteLine("Cancelled.\n"); return; }
        var selectedType = userTypes[typeChoice.Value - 1];

        UserService.AddUser(firstName, lastName, selectedType);
        Console.WriteLine($"\nUser {firstName} {lastName} ({selectedType}) added successfully!\n");
    }

    public static void AddDevice()
    {
        Console.WriteLine("\n--- Add a Device ---\n");
        PrintCancelHint();

        Console.WriteLine("Choose device type:");
        string[] deviceTypes = ["Camera", "Laptop", "Projector"];
        for (var i = 0; i < deviceTypes.Length; i++)
            Console.WriteLine($"{i + 1}. {deviceTypes[i]}");

        var selectedType = Utils.ReadCancellableInt(1, deviceTypes.Length);
        if (selectedType == null) { Console.WriteLine("Cancelled.\n"); return; }

        Console.WriteLine("\nEnter device name:");
        var nameInput = Utils.ReadCancellableString();
        if (nameInput == null) { Console.WriteLine("Cancelled.\n"); return; }
        var name = Utils.ToTitleCase(nameInput);

        Console.WriteLine("Choose availability status:");
        var statuses = Enum.GetValues<AvailabilityStatus>();
        for (var i = 0; i < statuses.Length; i++)
            Console.WriteLine($"{i + 1}. {statuses[i]}");
        var statusChoice = Utils.ReadCancellableInt(1, statuses.Length);
        if (statusChoice == null) { Console.WriteLine("Cancelled.\n"); return; }
        var status = statuses[statusChoice.Value - 1];

        Console.WriteLine("Enter age in years:");
        var age = Utils.ReadCancellableInt();
        if (age == null) { Console.WriteLine("Cancelled.\n"); return; }

        Console.WriteLine("Enter price in USD:");
        var price = Utils.ReadCancellableInt();
        if (price == null) { Console.WriteLine("Cancelled.\n"); return; }

        switch (selectedType.Value)
        {
            case 1:
                Console.WriteLine("Has 4K resolution? (y/n):");
                var has4KInput = Utils.ReadCancellableString();
                if (has4KInput == null) { Console.WriteLine("Cancelled.\n"); return; }
                var has4K = has4KInput.ToLower() is "y";
                Console.WriteLine("Hours on battery:");
                var hours = Utils.ReadCancellableInt();
                if (hours == null) { Console.WriteLine("Cancelled.\n"); return; }
                DeviceService.AddCamera(name, status, age.Value, price.Value, has4K, hours.Value);
                break;
            case 2:
                Console.WriteLine("Screen diagonal in inches:");
                var screen = Utils.ReadCancellableInt();
                if (screen == null) { Console.WriteLine("Cancelled.\n"); return; }
                Console.WriteLine("Weight in grams:");
                var weight = Utils.ReadCancellableInt();
                if (weight == null) { Console.WriteLine("Cancelled.\n"); return; }
                DeviceService.AddLaptop(name, status, age.Value, price.Value, screen.Value, weight.Value);
                break;
            case 3:
                Console.WriteLine("Brightness level:");
                var brightness = Utils.ReadCancellableInt();
                if (brightness == null) { Console.WriteLine("Cancelled.\n"); return; }
                Console.WriteLine("Optimal distance to screen (m):");
                var distance = Utils.ReadCancellableInt();
                if (distance == null) { Console.WriteLine("Cancelled.\n"); return; }
                DeviceService.AddProjector(name, status, age.Value, price.Value, brightness.Value, distance.Value);
                break;
        }

        Console.WriteLine($"\n{deviceTypes[selectedType.Value - 1]} '{name}' added successfully!\n");
    }

    public static void ShowAllDevices()
    {
        Console.WriteLine("\n--- All Devices ---\n");

        var devices = DeviceService.GetDevices();

        if (devices.Count == 0)
        {
            Console.WriteLine("No devices found.\n");
            return;
        }

        foreach (var device in devices)
            Console.WriteLine(device);

        Console.WriteLine();
    }

    public static void ShowAvailableDevices()
    {
        Console.WriteLine("\n--- Available Devices ---\n");

        var devices = DeviceService.GetAvailableDevices();

        if (devices.Count == 0)
        {
            Console.WriteLine("No available devices found.\n");
            return;
        }

        foreach (var device in devices)
            Console.WriteLine(device);

        Console.WriteLine();
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
        if (userChoice == null) { Console.WriteLine("Cancelled.\n"); return; }
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
        if (deviceChoice == null) { Console.WriteLine("Cancelled.\n"); return; }
        if (deviceChoice == 0)
        {
            AddDevice();
            devices = DeviceService.GetAvailableDevices();
            Console.WriteLine("\nSelect an available device:");
            for (var i = 0; i < devices.Count; i++)
                Console.WriteLine($"  {i + 1}. {devices[i]}");
            Console.WriteLine();
            deviceChoice = Utils.ReadCancellableInt(1, devices.Count);
            if (deviceChoice == null) { Console.WriteLine("Cancelled.\n"); return; }
        }
        var selectedDevice = devices[deviceChoice.Value - 1];

        Console.WriteLine("\nEnter rental date (yyyy-MM-dd) or press Enter for today:");
        var dateInput = Utils.ReadCancellableString();
        if (dateInput == null) { Console.WriteLine("Cancelled.\n"); return; }
        var rentalDate = string.IsNullOrEmpty(dateInput) ? DateTime.Today : DateTime.Parse(dateInput);

        Console.WriteLine("Enter rental length in days:");
        var rentalLength = Utils.ReadCancellableInt(min: 1);
        if (rentalLength == null) { Console.WriteLine("Cancelled.\n"); return; }

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
        if (choice == null) { Console.WriteLine("Cancelled.\n"); return; }
        var rental = activeRentals[choice.Value - 1];

        var fee = RentalService.ReturnDevice(rental.Id);

        Console.WriteLine($"\nDevice returned successfully!");
        if (fee > 0)
        {
            Console.WriteLine($"Late return fee: {fee} PLN");
            Console.WriteLine("Did the user pay the fee? y/n");
            var paid = Console.ReadLine();
            if (paid is "y" or "Y")
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
        if (choice == null) { Console.WriteLine("Cancelled.\n"); return; }
        var device = devices[choice.Value - 1];

        Console.WriteLine($"Enter a note (or press Enter to skip, '{Constants.CancelKeyword}' to cancel):");
        var note = Utils.ReadCancellableString();
        if (note == null) { Console.WriteLine("Cancelled.\n"); return; }

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
