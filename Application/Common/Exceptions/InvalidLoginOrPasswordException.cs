namespace Application.Common.Exceptions;

/// <summary>
/// This exception should be thrown when the password/login pair obtained is not correct.
/// </summary>
public class InvalidLoginOrPasswordException : Exception
{
    public InvalidLoginOrPasswordException() : base("Invalid login or password")
    {

    }
}