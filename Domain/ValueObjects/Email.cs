using System.ComponentModel;
using Domain.Primitives;

namespace Domain.ValueObjects;

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