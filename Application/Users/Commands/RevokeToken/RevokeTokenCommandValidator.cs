using FluentValidation;

namespace Application.Users.Commands.RevokeToken;

internal class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
{
    public RevokeTokenCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required!");
    }
}