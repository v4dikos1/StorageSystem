using MediatR;

namespace Application.Files.Queries.GetFile;

/// <summary>
/// A query for downloading a file from the repository
/// </summary>
/// <param name="FileId">file id</param>
public record GetFileQuery(Guid FileId) : IRequest<FileResponse> { }