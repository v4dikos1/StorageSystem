using Application.Common.Abstractions;
using Application.Common.Exceptions;
using AutoMapper;
using FluentValidation;
using MediatR;
using File = Application.Models.File;

namespace Application.Files.Queries.GetFileMarks;

internal class GetFileMarksQueryHandler : IRequestHandler<GetFileMarksQuery, FileMarksVm>
{
    private readonly IFileRepository _fileRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<GetFileMarksQuery> _validator;

    public GetFileMarksQueryHandler(IFileRepository fileRepository, IMapper mapper, IValidator<GetFileMarksQuery> validator)
    {
        _fileRepository = fileRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<FileMarksVm> Handle(GetFileMarksQuery request, CancellationToken cancellationToken)
    {
        // Validating data in the command
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var file = await _fileRepository.GetFileAsync(request.FileId);

        if (file == null)
        {
            throw new NotFoundException(nameof(File), request.FileId);
        }

        return _mapper.Map<FileMarksVm>(file);
    }
}