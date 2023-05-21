using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// The entity of the user of the system. 
/// </summary>
public class User : Entity
{
    private readonly List<File> _files = new();

    public User(Guid id, Username username, Email email) : base(id)
    {
        Username = username;
        Email = email;
    }

    public Username Username { get; set; }
    public Email Email { get; init; }

    public IReadOnlyCollection<File> Files => _files;

    public void AddFile(string name, string description)
    {
        var file = File.Create(name, description, Id);
        _files.Add(file);
    }

}