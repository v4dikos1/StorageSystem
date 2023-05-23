using FluentValidation;

namespace Application.Users.Commands.ConfirmEmail;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(c => c.VerificationToken).NotEmpty().WithMessage("Code is required!");
    }
}