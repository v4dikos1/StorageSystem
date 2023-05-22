namespace Application.Users.Commands.Login;

/// <summary>
/// A model for representing of the login command response
/// </summary>
/// <param name="Token">Access token</param>
/// <param name="RefreshToken">Refresh token</param>
public record LoginResponseVm(string Token, string RefreshToken) {}