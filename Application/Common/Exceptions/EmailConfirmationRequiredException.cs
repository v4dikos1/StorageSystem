namespace Application.Common.Exceptions;

public class EmailConfirmationRequiredException : Exception
{
    public EmailConfirmationRequiredException() : base("Need to confirm Email.")
    {
        
    }
}