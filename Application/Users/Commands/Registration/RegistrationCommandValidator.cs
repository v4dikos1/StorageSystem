using FluentValidation;

namespace Application.Users.Commands.Registration;

internal class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator()
    {
        RuleFor(u => u.Email.Value)
            .NotEmpty().WithMessage("Email is required!")
            .EmailAddress().WithMessage("Invalid Email!");

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Password is required!");

        RuleFor(u => u.Username.Value)
            .NotEmpty()
            .WithMessage("Username is required!");
    }
}