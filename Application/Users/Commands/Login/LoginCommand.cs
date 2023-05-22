using MediatR;

namespace Application.Users.Commands.Login;

/// <summary>
/// The command to be called for user authentication and authorization, as well as for getting a token
/// </summary>
/// <param name="Email">User email</param>
/// <param name="Password">User password</param>
/// <returns>token</returns>
public record LoginCommand(string Email,  string Password) : IRequest<string> {}