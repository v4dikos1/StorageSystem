using Domain.Primitives;

namespace Domain.ValueObjects;

/// <summary>
/// Filename value object
/// </summary>
public class Filename : ValueObject
{
    /// <summary>
    /// Maximum filename length
    /// </summary>
    public static readonly int MaxLength = 20;

    /// <summary>
    /// Default filename value
    /// </summary>
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