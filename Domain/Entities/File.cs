using Domain.Primitives;

namespace Domain.Entities;

public class File : Entity
{
    internal File(Guid id, string name, string description, DateTime createdAt, Guid ownerId) : base(id)
    {
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        OwnerId = ownerId;
    }

    public Guid OwnerId { get; private set; }

    public string Name { get; private set; }
    public string Description { get; private set; }

    public DateTime CreatedAt {get; private set; }

    public static File Create(
        string name,
        string description,
        Guid ownerId)
    {
        File file = new(Guid.NewGuid(), name, description, DateTime.Now, ownerId);

        return file;
    }
}