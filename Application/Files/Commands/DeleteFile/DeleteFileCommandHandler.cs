﻿using Application.Common.Abstractions;
using Application.Common.Exceptions;
using FluentValidation;
using MediatR;
using File = Application.Models.File;

namespace Application.Files.Commands.DeleteFile;

internal class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand>
{
    private readonly IFileRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public DeleteFileCommandHandler(IFileRepository repository, IUnitOfWork unitOfWork, IFileService fileService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        // checking if the file exists
        var file = await _repository.GetFileAsync(request.Id);

        if (file == null)
        {
            throw new NotFoundException(nameof(File), request.Id);
        }

        // If the id of the file creator does not match the id of the user trying to delete the file
        if (file.OwnerId != request.CurrentUserId)
        {
            throw new IllegalOperationException();
        }

        // deleting from the storage
        await _fileService.DeleteFileAsync(file.FileNameInStorage);

        // deleting from database
        _repository.DeleteFile(request.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}