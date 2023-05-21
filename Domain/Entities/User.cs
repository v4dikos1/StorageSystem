using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// The entity of the user of the system. 
/// </summary>
public class User : Entity
{
    public User(Guid id, Username username, Email email) : base(id)
    {
        Username = username;
        Email = email;
    }

    public Username Username { get; set; }
    public Email Email { get; set; }

}