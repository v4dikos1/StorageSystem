using Application.Common.Abstractions;
using Application.Common.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Files.Queries.GetFileInfo;

internal class GetFileInfoQueryHandler : IRequestHandler<GetFileInfoQuery, FileInfo>
{
    private readonly IFileRepository _fileRepository;
    private readonly FileStorageOptions _storageOptions;
    private readonly IMapper _mapper;

    public GetFileInfoQueryHandler(IFileRepository fileRepository,
        IOptions<FileStorageOptions> storageOptions, IMapper mapper)
    {
        _fileRepository = fileRepository;
        _mapper = mapper;
        _storageOptions = storageOptions.Value;
    }

    public async Task<FileInfo> Handle(GetFileInfoQuery request, CancellationToken cancellationToken)
    {
        // checking if the file exists
        var file = await _fileRepository.GetFileAsync(request.Id);

        if (file == null)
        {
            throw new NotFoundException(nameof(File), request.Id);
        }

        // mapping
        return _mapper.Map<Models.File, FileInfo>(file,
            opt => opt.Items["ReceiptLink"] = _storageOptions.ReceiptLink);
    }
}