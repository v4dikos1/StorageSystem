using MediatR;

namespace Application.Files.Queries.GetFileInfo;

/// <summary>
/// A query to get information about a file by its id
/// </summary>
/// <param name="Id">file Id</param>
public record GetFileInfoQuery(Guid Id) : IRequest<FileInfo> { }