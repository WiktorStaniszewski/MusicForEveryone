namespace User.Domain.Exceptions;

public class UserDoesntExistsExeption : Exception
{
    public UserDoesntExistsExeption(string message) : base(message) { }
    public UserDoesntExistsExeption() : base("User does not exists in the system.") { }
}
