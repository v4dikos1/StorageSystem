using Application.Common.Abstractions;
using Application.Common.Exceptions;
using Application.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Files.Commands.UploadFile;

internal class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;
    private readonly FileStorageOptions _fileStorageOptions;
    private readonly IValidator<UploadFileCommand> _validator;

    public UploadFileCommandHandler(IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IFileService fileService,
        IFileRepository fileRepository,
        IOptions<FileStorageOptions> fileStorageOptions, IValidator<UploadFileCommand> validator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _fileRepository = fileRepository;
        _validator = validator;
        _fileStorageOptions = fileStorageOptions.Value;

    }

    public async Task<Guid> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        // Validating data in the command
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // if file creator exists
        var creator = await _userRepository.GetUserByIdAsync(request.CreatorId, cancellationToken);

        if (creator == null)
        {
            throw new NotFoundException(nameof(User), request.CreatorId);
        }

        var fileId = Guid.NewGuid();
        var fileName = fileId + Path.GetExtension(request.File.FileName);

        // uploading file to the storage
        var result = await _fileService.UploadFileAsync(fileName, request.File);

        // if there are errors when downloading the file
        if (result != true) throw new FileUploadException();

        // saving file metadata
        _fileRepository.AddFile(
            fileId,
            request.Name,
            request.Description ?? "",
            _fileStorageOptions.ServiceUrl + "/" + _fileStorageOptions.BucketName + "/" + fileId + Path.GetExtension(request.File.FileName),
            fileName,
            request.ToAutoDelete ?? false, 
            creator.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return fileId;
    }
}