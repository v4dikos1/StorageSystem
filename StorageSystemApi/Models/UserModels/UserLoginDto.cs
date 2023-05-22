using Application.Common.Mapping;
using Application.Users.Commands.Login;
using AutoMapper;

namespace StorageSystemApi.Models.UserModels;

/// <summary>
/// A dto model for user login
/// </summary>
public record UserLoginDto : IMapWith<LoginCommand>
{
    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserLoginDto, LoginCommand>();
    }
}