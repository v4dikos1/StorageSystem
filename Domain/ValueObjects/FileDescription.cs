using Domain.Primitives;

namespace Domain.ValueObjects;

/// <summary>
/// File description value object
/// </summary>
public class FileDescription : ValueObject
{
    /// <summary>
    /// Maximum description length
    /// </summary>
    public static readonly int MaxLength = 200;

    /// <summary>
    /// Default description value
    /// </summary>
    public static readonly string DefaultValue = "";

    public FileDescription(string value)
    {
        Value = value;
    }

    public string Value { get; set; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}