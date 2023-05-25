using FluentValidation;

namespace Application.Files.Queries.GetFileInfo;

internal class GetFileInfoQueryValidator : AbstractValidator<GetFileInfoQuery>
{
    public GetFileInfoQueryValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("File id is required!");
    }
}