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
}
