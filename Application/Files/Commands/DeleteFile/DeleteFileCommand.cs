using MediatR;

namespace Application.Files.Commands.DeleteFile;

/// <summary>
/// Command for deleting a file from the storage
/// </summary>
/// <param name="Id">File id</param>
public record DeleteFileCommand(Guid Id, Guid CurrentUserId) : IRequest{}