using Application.Common.Abstractions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using File = Application.Models.File;
using FileInfo = Application.Files.Queries.GetFileInfo.FileInfo;

namespace Application.Files.Queries.GetFiles;

internal class GetFilesQueryHandler : IRequestHandler<GetFilesQuery, FileListVm>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly FileStorageOptions _storageOptions;

    public GetFilesQueryHandler(IUserRepository userRepository, IMapper mapper, IOptions<FileStorageOptions> storageOptions)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _storageOptions = storageOptions.Value;
    }

    public async Task<FileListVm> Handle(GetFilesQuery request, CancellationToken cancellationToken)
    {
        var files = await _userRepository.GetUserFilesAsync(request.UserId,
            request.Offset, request.Limit, cancellationToken);

        return new FileListVm
        {
            UserId = request.UserId,
            Count = files.Count,
            Files = _mapper.Map<List<File>, List<FileInfo>>(files, opts =>
                opts.Items["ReceiptLink"] = _storageOptions.ReceiptLink)
        };
    }
}