using Application.Common.Mapping;
using Application.Users.Commands.RefreshToken;
using AutoMapper;

namespace StorageSystemApi.Models.TokenModels;

/// <summary>
/// A model for token update
/// </summary>
public record TokenRefreshDto : IMapWith<RefreshTokenCommand>
{
    /// <summary>
    /// An access token
    /// </summary>
    public required string AccessToken { get; set; }

    /// <summary>
    /// A refresh token
    /// </summary>
    public required string RefreshToken { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TokenRefreshDto, RefreshTokenCommand>();
    }
}