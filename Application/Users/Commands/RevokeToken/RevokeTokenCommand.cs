using MediatR;

namespace Application.Users.Commands.RevokeToken;

/// <summary>
/// Command to revoke the refresh token
/// </summary>
/// <param name="Id">User id</param>
public record RevokeTokenCommand(Guid Id): IRequest {}