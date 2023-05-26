using FileInfo = Application.Files.Queries.GetFileInfo.FileInfo;

namespace Application.Files.Queries.GetFiles;

public class FileListVm
{
    public Guid UserId { get; set; }
    public List<FileInfo> Files { get; set; } = new();
    public int Count { get; set; }
}