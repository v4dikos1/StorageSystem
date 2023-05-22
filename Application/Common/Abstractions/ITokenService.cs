using Application.Models;

namespace Application.Common.Abstractions;

/// <summary>
/// Service for JWT token generation
/// </summary>
public interface ITokenService
{
    string Create(User  user);
}