namespace Application.Common.Exceptions;

public class IllegalOperationException : Exception
{
    public IllegalOperationException() : base("A file can only be deleted by its creator.")
    {
        
    }
}