namespace cwiczenia2.Model.Users;

public class User
{
    private static int _id = 0;
    private static List<User> _users = new List<User>();
    public static IReadOnlyList<User> Users => _users;
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public UserType UserType { get; set; }

    public User(
        string name,
        string lastName,
        UserType userType
    ) {
        Id = ++_id;
        Name = name;
        LastName = lastName;
        UserType = userType;
        _users.Add(this);
    }
}