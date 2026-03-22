namespace cwiczenia2.Model.Users;

public class User
{
    private static int _id = 0;
    private static readonly List<User> _users = [];
    public static IReadOnlyList<User> Users => _users;
    public int Id { get; init; }
    public string FirstName { get; }
    public string LastName { get; }
    public UserType UserType { get; }
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