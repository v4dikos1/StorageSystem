namespace Application.Common.Exceptions;

/// <summary>
/// This exception should be thrown when some entity with a given key already exists.
/// </summary>
public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string name, object key) : base($"Entity \"{name}\" with ({key}) already exists."){}
}