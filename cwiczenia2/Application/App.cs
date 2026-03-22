namespace cwiczenia2.Application;

public class App
{
    private int SelectedAction { get; set; }

    public void Run()
    {
        var isRunning = true;
        
        GreetUser();
        
        while (isRunning)
        {
            GetUserAction();

            if (SelectedAction == 0)
            {
                Console.WriteLine("\nAre you sure? y/n");
                var confirm = Console.ReadLine();
                if (confirm is "y" or "Y")
                {
                    isRunning = false;
                }
                else
                {
                    ShowAvailableActions();
                }
            }
            else
            {
                switch (SelectedAction)
                {
                    case 1:
                        AppActions.AddUser();
                        break;
                    case 2:
                        AppActions.AddDevice();
                        break;
                    case 3:
                        AppActions.ShowAllDevices();
                        break;
                    case 4:
                        AppActions.ShowAvailableDevices();
                        break;
                    case 5:
                        AppActions.RentDevice();
                        break;
                    case 6:
                        AppActions.ReturnDevice();
                        break;
                    case 7:
                        AppActions.MarkDeviceUnavailable();
                        break;
                    case 8:
                        AppActions.ShowActiveRentalsForUser();
                        break;
                    case 9:
                        AppActions.ShowExpiredRentals();
                        break;
                    case 10:
                        AppActions.GenerateReport();
                        break;
                }

                ShowAvailableActions();
            }
        }
        
        Console.WriteLine("\nThanks for using the Rental App. Goodbye!\n");
    }

    private static void GreetUser()
    {
        Console.WriteLine("\nWelcome to the Rental App!\n");
        Console.WriteLine("Choose your action by inserting it's number:");
        ShowAvailableActions();
    }

    private static void ShowAvailableActions()
    {
        Console.WriteLine();
        for (var i = 0; i < Constants.AppActions.Length; i++)
            Console.WriteLine($"{i + 1}. {Constants.AppActions[i]}");
        Console.WriteLine("0. Exit Rental App\n");
    }

    private void GetUserAction()
    {
        SelectedAction = Utils.ReadInt(0, Constants.AppActions.Length);
    }
}