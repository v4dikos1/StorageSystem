using FluentValidation;

namespace Application.Files.Commands.UploadFile;

internal class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(c => c.File).NotEmpty().WithMessage("File is required!");
        RuleFor(c => c.Name).NotEmpty().WithMessage("File name is required!");
        RuleFor(c => c.CreatorId).NotEmpty().WithMessage("CreatorId field is required!");
    }
}