using cwiczenia2.Data;
namespace cwiczenia2.Application;

public class App
{
    private int SelectedAction { get; set; }

    public void Run()
    {
        DataStore.LoadData();
        var isRunning = true;

        GreetUser();
        
        while (isRunning)
        {
            GetUserAction();

            switch (SelectedAction)
            {
                case 0:
                    if (Utils.ConfirmAction())
                        isRunning = false;
                    break;
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

            if (isRunning)
                ShowAvailableActions();
        }
        
        DataStore.SaveData();
        SayGoodbye();
    }

    private static void GreetUser()
    {
        Console.WriteLine("\nWelcome to the Rental App!\n");
        Console.WriteLine("Choose your action by inserting it's number:");
        ShowAvailableActions();
    }
    
    private static void SayGoodbye()
    {
        Console.WriteLine("\nThanks for using the Rental App. Goodbye!\n");
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
