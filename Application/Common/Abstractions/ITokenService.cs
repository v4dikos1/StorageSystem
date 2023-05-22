using System.Security.Claims;

namespace Application.Common.Abstractions;

/// <summary>
/// Service for JWT token generation
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Create an access token
    /// </summary>
    /// <param name="claims">Claims collection</param>
    /// <returns>Access token (string)</returns>
    string Create(IEnumerable<Claim> claims);

    /// <summary>
    /// Create a refresh token
    /// </summary>
    /// <returns>RefreshToken</returns>
    string CreateRefreshToken();

    /// <summary>
    /// Retrieving principals from a token
    /// </summary>
    /// <param name="token">Access token</param>
    /// <returns>ClaimsPrincipal</returns>
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}