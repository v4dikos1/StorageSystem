using Application.Users.Commands.Login;
using MediatR;

namespace Application.Users.Commands.RefreshToken;

/// <summary>
/// Command called to update the token
/// </summary>
/// <param name="AccessToken">Access token to update</param>
/// <param name="RefreshToken">Refresh token to update</param>
/// <returns>A new access token and a new refresh token</returns>
public record RefreshTokenCommand (string AccessToken,  string RefreshToken) : IRequest<LoginResponseVm> {} 