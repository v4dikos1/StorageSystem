using FluentValidation;

namespace Application.Files.Queries.GetFileMarks;

internal class GetFileMarksQueryValidator : AbstractValidator<GetFileMarksQuery>
{
    public GetFileMarksQueryValidator()
    {
        RuleFor(q => q.FileId).NotEmpty().WithMessage("File id is required!");
    }
}