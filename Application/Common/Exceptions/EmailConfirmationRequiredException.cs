namespace Application.Common.Exceptions;

/// <summary>
/// Exception thrown when a user with an unconfirmed email tries to access protected resources
/// </summary>
public class EmailConfirmationRequiredException : Exception
{
    public EmailConfirmationRequiredException() : base("Need to confirm Email.")
    {
        
    }
}