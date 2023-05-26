using FluentValidation;

namespace Application.Users.Commands.ConfirmEmail;

internal class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(c => c.VerificationToken).NotEmpty().WithMessage("Code is required!");
    }
}