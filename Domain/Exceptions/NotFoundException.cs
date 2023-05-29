namespace Domain.Exceptions;

/// <summary>
/// This exception should be thrown when some entity was not found by a given key.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) not found.")
    {

    }
}