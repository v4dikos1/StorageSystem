using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Files.Commands.UploadFile;

/// <summary>
/// Command to upload a file to the storage
/// </summary>
/// <param name="File">A file to upload</param>
/// <param name="CreatorId">File creator id</param>
/// <param name="ToAutoDelete">Automatically delete a file</param>
/// <param name="Name">File name</param>
/// <param name="Description">File description</param>
public record UploadFileCommand(IFormFile  File, Guid CreatorId, bool? ToAutoDelete, string Name, string? Description) : IRequest<Guid>
{
}