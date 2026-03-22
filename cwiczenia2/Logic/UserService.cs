using cwiczenia2.Model.Users;

namespace cwiczenia2.Logic;

public static class UserService
{
    public static void AddUser(string firstName, string lastName, UserType userType)
    {
        new User(firstName, lastName, userType);
    }

    public static IReadOnlyList<User> GetUsers()
    {
        return User.Users;
    }
}
