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
                // TODO: handle actions 1-10
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
        var actionChosen = false;
        
        while (!actionChosen)
        {
            var input = Console.ReadLine();

            if (int.TryParse(input, out var action) && action >= 0 && action <= Constants.AppActions.Length)
            {
                SelectedAction = action;
                actionChosen = true;
            }
            else
            {
                Console.WriteLine("\nInput incorrect, try again:\n");
                ShowAvailableActions();
            }
        }
    }
}