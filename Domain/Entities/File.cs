using Domain.Primitives;

namespace Domain.Entities;

/// <summary>
/// A file entity created to encapsulate the business logic associated with files.
/// However, in this case, it does not contain any logic and is only used for now to save the architecture style
/// and the possibility to disperse/use it in the future. 
/// </summary>
public class File : Entity
{
    internal File(Guid id, string name, string description, DateTime createdAt, Guid ownerId) : base(id)
    {
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        OwnerId = ownerId;
    }

    /// <summary>
    /// File creator
    /// </summary>
    public Guid OwnerId { get; private set; }

    /// <summary>
    /// File name
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// File description
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// File creation date
    /// </summary>
    public DateTime CreatedAt {get; private set; }

    /// <summary>
    /// Static factory method for creating a file
    /// </summary>
    /// <param name="name">File name</param>
    /// <param name="description">File description</param>
    /// <param name="ownerId">File creator id</param>
    /// <returns>File</returns>
    public static File Create(
        string name,
        string description,
        Guid ownerId)
    {
        File file = new(Guid.NewGuid(), name, description, DateTime.Now, ownerId);

        return file;
    }
}