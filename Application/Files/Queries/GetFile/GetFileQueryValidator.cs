using FluentValidation;

namespace Application.Files.Queries.GetFile;

internal class GetFileQueryValidator : AbstractValidator<GetFileQuery>
{
    public GetFileQueryValidator()
    {
        RuleFor(c => c.FileId).NotEmpty().WithMessage("File id is required!");
    }
}