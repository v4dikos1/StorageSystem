using Application.Common.Abstractions;
using Application.Common.Exceptions;
using AutoMapper;
using MediatR;
using File = Application.Models.File;

namespace Application.Files.Queries.GetFileMarks;

internal class GetFileMarksQueryHandler : IRequestHandler<GetFileMarksQuery, FileMarksVm>
{
    private readonly IFileRepository _fileRepository;
    private readonly IMapper _mapper;

    public GetFileMarksQueryHandler(IFileRepository fileRepository, IMapper mapper)
    {
        _fileRepository = fileRepository;
        _mapper = mapper;
    }

    public async Task<FileMarksVm> Handle(GetFileMarksQuery request, CancellationToken cancellationToken)
    {
        var file = await _fileRepository.GetFileAsync(request.FileId);

        if (file == null)
        {
            throw new NotFoundException(nameof(File), request.FileId);
        }

        return _mapper.Map<FileMarksVm>(file);
    }
}