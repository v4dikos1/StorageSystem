using Domain.Primitives;

namespace Domain.ValueObjects;

/// <summary>
/// Email value object
/// </summary>
public class Email : ValueObject
{
    public Email(string value)
    {
        var result = IsValidEmail(value);

        if (!result) throw new ArgumentException("Invalid email.");

        Value = value;
    }

    public string Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    /// <summary>
    /// A method for checking email validity
    /// </summary>
    /// <param name="email">Email string</param>
    /// <returns>true if email is valid</returns>
    bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }
        try
        {
            var address = new System.Net.Mail.MailAddress(email);
            return address.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}