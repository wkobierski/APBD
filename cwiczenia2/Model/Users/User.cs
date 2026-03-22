namespace cwiczenia2.Model.Users;

public class User
{
    private static int _id = 0;
    private static List<User> _users = new List<User>();
    public static IReadOnlyList<User> Users => _users;
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserType UserType { get; set; }
    public int TotalLateFees { get; set; }

    public User(
        string firstName,
        string lastName,
        UserType userType
    ) {
        Id = ++_id;
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
        _users.Add(this);
    }

    public static void Reset()
    {
        _users.Clear();
        _id = 0;
    }

    public static void SetIdCounter(int value)
    {
        _id = value;
    }

    public override string ToString()
    {
        var result = $"[{Id}] {FirstName} {LastName} | Type: {UserType}";
        if (TotalLateFees > 0)
            result += $" | Late fees: {TotalLateFees} PLN";
        return result;
    }
}