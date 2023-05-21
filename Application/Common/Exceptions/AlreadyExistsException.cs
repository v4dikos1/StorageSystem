namespace Application.Common.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string name, object key) : base($"Entity \"{name}\" with ({key}) already exists."){}
}