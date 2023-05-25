using Domain.ValueObjects;

namespace Application.Files.Queries.GetFileInfo;

/// <summary>
/// A model that represents information about the file
/// </summary>
public class FileInfo
{
    public Guid Id { get; set; }

    public string Name { get; set; } = Filename.DefaultValue;
    public string Description { get; set; } = FileDescription.DefaultValue;

    // Download link
    public string Url { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public required Guid OwnerId { get; set; }

    public bool ToAutoDelete { get; set; } = false;
}