using FluentValidation;

namespace Application.Users.Queries.GetUserProfile;

internal class GetUserProfileQueryValidator : AbstractValidator<GetUserProfileQuery>
{
    public GetUserProfileQueryValidator()
    {
        RuleFor(q => q.Id).
            NotEmpty().WithMessage("Id is required.");
    }
}