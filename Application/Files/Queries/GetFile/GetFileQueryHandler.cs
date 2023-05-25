using Application.Common.Abstractions;
using Application.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Files.Queries.GetFile;

internal class GetFileQueryHandler : IRequestHandler<GetFileQuery, FileResponse>
{
    private readonly IFileRepository _fileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IValidator<GetFileQuery> _validator;

    public GetFileQueryHandler(IFileRepository fileRepository, IUnitOfWork unitOfWork, IFileService fileService, IValidator<GetFileQuery> validator)
    {
        _fileRepository = fileRepository;
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _validator = validator;
    }

    public async Task<FileResponse> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        // Validating data in the command
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var fileInfo = await _fileRepository.GetFileAsync(request.FileId);

        // if the file exists
        if (fileInfo == null)
        {
            throw new NotFoundException(nameof(File), request.FileId);
        }

        // If the file already has a deletion marker,
        // it means that the appropriate commands to delete the file have not been called.
        // (Probably, connection breakage during file loading and as a consequence the hub method was not called)
        if (fileInfo.RemovalMark)
        {
            // In that case you can still delete the file no matter what:
            // await _mediator.Send(new DeleteFileCommand(request.FileId));

            // But this option is inexpedient. 
            // TODO: This part needs to be fixed, the logic of deleting a file marked for deletion is not fully thought out.

            throw new Exception("A file already marked to delete");
        }

        // If the file is marked to be automatically deleted, then set the removal marker
        if (fileInfo.ToAutoDelete)
        {
            fileInfo.RemovalMark = true;
            fileInfo.RemovalMarkDate = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        // getting a file stream
        var fileStream = await _fileService.GetFileAsync(fileInfo.FileNameInStorage);

        return new FileResponse
        {
            FileName = fileInfo.Name + Path.GetExtension(fileInfo.FileNameInStorage),
            ToDelete = fileInfo.ToAutoDelete,
            FileStream = fileStream
        };
    }
}