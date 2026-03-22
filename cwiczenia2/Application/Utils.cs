using System.Globalization;
namespace cwiczenia2.Application;

public static class Utils
{
    public static string ToTitleCase(string input)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.Trim().ToLower());
    }

    public static int ReadInt(int? min = null, int? max = null)
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out var value)
                && (min == null || value >= min)
                && (max == null || value <= max))
                return value;
            Console.WriteLine("\nInput incorrect, try again:");
        }
    }

    public static int? ReadCancellableInt(int? min = null, int? max = null)
    {
        while (true)
        {
            var input = Console.ReadLine()?.Trim();
            if (string.Equals(input, Constants.CancelKeyword, StringComparison.OrdinalIgnoreCase))
                return null;
            if (int.TryParse(input, out var value)
                && (min == null || value >= min)
                && (max == null || value <= max))
                return value;
            Console.WriteLine($"\nInput incorrect, try again (or type '{Constants.CancelKeyword}' to cancel):");
        }
    }

    public static string? ReadCancellableString()
    {
        var input = Console.ReadLine()?.Trim();
        if (string.Equals(input, Constants.CancelKeyword, StringComparison.OrdinalIgnoreCase))
            return null;
        return input ?? "";
    }

    public static bool ConfirmAction()
    {
        Console.WriteLine("\nAre you sure? y/n");
        var input = Console.ReadLine();
        return input is "y" or "Y";
    }
}
