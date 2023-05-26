using FluentValidation;

namespace Application.Users.Commands.Login;

internal class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required!")
            .EmailAddress().WithMessage("Email is incorrect!");

        RuleFor(c => c.Password).NotEmpty().WithMessage("Password is required!");
    }
}