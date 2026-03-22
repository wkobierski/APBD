using cwiczenia2.Model.Users;
namespace cwiczenia2.Data;

public class UserData
{
    public int Id { get; init; }
    public string FirstName { get; init; } = "";
    public string LastName { get; init; } = "";
    public UserType UserType { get; init; }
    public int TotalLateFees { get; init; }
}
