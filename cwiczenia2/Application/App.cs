namespace cwiczenia2.Application;

public class App
{
    public int SelectedAction { get; set; }

    public void Run()
    {
        GreetUser();
        GetUserSelection();
    }

    private void GreetUser()
    {
        Console.WriteLine("Welcome to the Rental App!");
        Console.WriteLine("Choose your action by inserting it's number: ");
        // TODO create a map/dict where key is action number and value is the description
        Console.WriteLine("1. Add a user to the system");
        Console.WriteLine("2. Add a device of a given type");
        Console.WriteLine("3. Show a list of all devices with their statuses");
        Console.WriteLine("4. Show devices available to rent");
        Console.WriteLine("5. Rent a device to a user");
        Console.WriteLine("6. Return a device and calculate late return fee");
        Console.WriteLine("7. Mark device non-available");
        Console.WriteLine("8. Show all active rentals for user");
        Console.WriteLine("9. Show all expired rentals");
        Console.WriteLine("10. Generate a summary report");
    }

    private void GetUserSelection()
    {
        SelectedAction = Console.Read();
    }
}