using FluentValidation;

namespace Application.Files.Commands.DeleteFile;

internal class DeleteFileCommandValidator : AbstractValidator<DeleteFileCommand>
{
    public DeleteFileCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("File id is required!");
    }
}