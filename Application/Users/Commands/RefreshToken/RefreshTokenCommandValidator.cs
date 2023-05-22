using FluentValidation;

namespace Application.Users.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(c => c.AccessToken).NotEmpty().WithMessage("Access token is required!");
        RuleFor(c => c.RefreshToken).NotEmpty().WithMessage("Refresh token is required!");
    }
}