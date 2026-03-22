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

    public static void ClearFees(int userId)
    {
        var user = User.Users.First(u => u.Id == userId);
        user.TotalLateFees = 0;
    }
}
