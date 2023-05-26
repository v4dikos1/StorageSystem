using Application.Common.Abstractions;
using Application.Common.Exceptions;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Files.Queries.GetFileInfo;

internal class GetFileInfoQueryHandler : IRequestHandler<GetFileInfoQuery, FileInfo>
{
    private readonly IFileRepository _fileRepository;
    private readonly FileStorageOptions _storageOptions;
    private readonly IValidator<GetFileInfoQuery> _validator;
    private readonly IMapper _mapper;

    public GetFileInfoQueryHandler(IFileRepository fileRepository,
        IOptions<FileStorageOptions> storageOptions, IValidator<GetFileInfoQuery> validator, IMapper mapper)
    {
        _fileRepository = fileRepository;
        _validator = validator;
        _mapper = mapper;
        _storageOptions = storageOptions.Value;
    }

    public async Task<FileInfo> Handle(GetFileInfoQuery request, CancellationToken cancellationToken)
    {
        // Validating data in the command
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

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