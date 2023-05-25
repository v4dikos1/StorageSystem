using File = Application.Models.File;

namespace Application.Common.Abstractions;


public interface IFileRepository
{
    void AddFile(Guid id, string name, string description, string url, string fileNameInStorage, bool toAutoDelete, Guid ownerId);
    Task<File?> GetFileAsync(Guid id);
    void DeleteFile(Guid id);
}