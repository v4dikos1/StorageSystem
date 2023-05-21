namespace Domain.Primitives;

/// <summary>
/// A class for representing value objects.
/// Such objects are used to wrap over primitive types to avoid an error,
/// such as passing parameters to the constructor of a class that uses a value object.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();

    // The method returns a collection of primitive values that represent an object-value
    public bool Equals(ValueObject? other)
    {
        return other is not null && ValuesAreEqual(other);
    }

    public override bool Equals(object? obj)
    {
        return obj is ValueObject other && ValuesAreEqual(other);
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(default(int), HashCode.Combine);
    }

    //Two value objects are equal if their list of primitives is also equal
    public bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }
}