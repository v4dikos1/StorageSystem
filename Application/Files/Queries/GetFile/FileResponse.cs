namespace Application.Files.Queries.GetFile;

/// <summary>
/// A model for representing a file
/// </summary>
public class FileResponse
{
    public Guid FileId { get; set; }
    public string FileName { get; set; } = null!;
    public bool ToDelete { get; set; }

    public Stream FileStream { get; set; } = null!;
}