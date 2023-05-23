using MediatR;

namespace Application.Users.Commands.ConfirmEmail;

/// <summary>
/// Command to confirm email
/// </summary>
/// <param name="VerificationToken">Code sent to the email</param>
public record ConfirmEmailCommand(string VerificationToken) : IRequest{}