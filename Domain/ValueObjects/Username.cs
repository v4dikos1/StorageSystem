using Domain.Primitives;

namespace Domain.ValueObjects;

public sealed class Username : ValueObject
{
    public const int MaxLength = 15;

    public Username(string value)
    {
        if (value.Length > MaxLength)
        {
            throw new ArgumentException("Username is too long.");
        }

        Value = value;
    }

    public string Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}