using MediatR;

namespace Application.Files.Queries.GetFileMarks;

/// <summary>
/// Request for file marks
/// </summary>
/// <param name="FileId">file id</param>
public record GetFileMarksQuery(Guid FileId) : IRequest<FileMarksVm> { }