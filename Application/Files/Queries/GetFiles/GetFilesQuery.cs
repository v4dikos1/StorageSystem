using MediatR;

namespace Application.Files.Queries.GetFiles;

public record GetFilesQuery (Guid UserId, int Offset, int Limit): IRequest<FileListVm> {}