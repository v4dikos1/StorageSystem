using Domain.Primitives;

namespace Domain.ValueObjects;

public class Filename : ValueObject
{
    public static readonly int MaxLength = 20;
    public static readonly string DefaultValue = "New file";

    public Filename(string value)
    {
        Value = value;
    }

    public string Value { get; set; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}