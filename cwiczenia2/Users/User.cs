namespace cwiczenia2.Users;

public enum UserType
{
    Student,
    Employee,
}

public class User
{
    private static int _id = 0;
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
    }
}