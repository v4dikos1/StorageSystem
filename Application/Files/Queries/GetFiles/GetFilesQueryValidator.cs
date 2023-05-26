using FluentValidation;

namespace Application.Files.Queries.GetFiles;

internal class GetFilesQueryValidator : AbstractValidator<GetFilesQuery>
{
    public GetFilesQueryValidator()
    {
        RuleFor(q => q.UserId).NotEmpty().WithMessage("UserId is required!");

        RuleFor(q => q.Offset).GreaterThanOrEqualTo(0);

        RuleFor(q => q.Limit).GreaterThan(0);
    }
}