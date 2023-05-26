using Application.Common.Abstractions;
using Application.Common.Exceptions;
using File = Application.Models.File;

namespace Persistence.Repositories;

internal sealed class FileRepository : IFileRepository
{
    private readonly IApplicationDbContext _dbContext;

    public FileRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddFile(Guid id, string name, string description, string url, string fileNameInStorage, bool toAutoDelete, Guid ownerId)
    {
        _dbContext.Files.Add(new File
        {
            Id = id,
            Name = name,
            Description = description,
            Path = url,
            ToAutoDelete = toAutoDelete,
            FileNameInStorage = fileNameInStorage,
            OwnerId = ownerId,
            CreatedAt = DateTime.UtcNow
        });
    }

    public async Task<File?> GetFileAsync(Guid id)
    {
        return await _dbContext.Files.FindAsync(id);
    }

    public async void DeleteFile(Guid id)
    {
        var file = await _dbContext.Files.FindAsync(id);

        if (file is null) throw new NotFoundException(nameof(File), id);

        _dbContext.Files.Remove(file);
    }
}